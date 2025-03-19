using HR.LeaveManagement.Identity.Models;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly JwtSettings _jwtSettings;

	public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
		IOptions<JwtSettings> jwtSettings)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_jwtSettings = jwtSettings.Value;
	}

	public async Task<AuthResponse> Login(AuthRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user is null)
			throw new NotFoundException($"User with {request.Email} not found.",request.Email);

		var results = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

		if (results.Succeeded == false)
			throw new BadRequestException($"Credentials for {request.Email} are not valid.");

		JwtSecurityToken token = await GenerateToken(user);

		var response = new AuthResponse
		{
			Id = user.Id,
			Email = user.Email,
			Username = user.UserName,
			Token = new JwtSecurityTokenHandler().WriteToken(token)
		};
		return response;
	}

	private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
	{
		var userClaims = await _userManager.GetClaimsAsync(user);
		var roles = await _userManager.GetRolesAsync(user);

		var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim("uid", user.Id)
		}
		.Union(userClaims)
		.Union(roleClaims);

		var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

		var signinCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

		return new JwtSecurityToken(
				issuer: _jwtSettings.Issue,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: signinCredentials
			);
	}

	public async Task<RegisterReponse> Register(RegisterRequest request)
	{
		var user = new ApplicationUser
		{
			Email = request.Email,
			Firstname = request.Firstname,
			Lastname = request.Lastname,
			UserName = request.Username,
			EmailConfirmed = true
		};

		var result = await _userManager.CreateAsync(user, request.Password);

		if (result.Succeeded)
		{
			StringBuilder sb = new StringBuilder();

			foreach (var err in result.Errors)
				sb.AppendFormat("*{0}", err.Description);

			throw new BadRequestException($"{sb}");
		}
			

		await _userManager.AddToRoleAsync(user, "Employee");
		return new RegisterReponse() { UserId = user.Id };
	}
}
