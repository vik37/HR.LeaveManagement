using HR.LeaveManager.Application.Models.Identities;

namespace HR.LeaveManager.Application.Contracts.Identity;

public interface IAuthService
{
	Task<AuthResponse> Login(AuthRequest request);
	Task<RegisterReponse> Register(RegisterRequest request);
}
