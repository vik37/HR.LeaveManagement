# HR.LeaveManagement

## 📌 Overview
HR.LeaveManagement is a leave management system built with **.NET** technologies. This project was initially inspired by a Udemy course, "Clean Architecture", but has been significantly enhanced with **bug fixes, additional features, and unit tests** to improve functionality and performance.

## 🚀 Technologies Used
- **.NET 8**
- **C#** (Backend)
- **Blazor** (Frontend)
- **NSwag Studio**
- **Entity Framework Core** (Database Handling)
- **MSSQL** (Database)
- **RESTful APIs** (Integration)
- **Github** (Optional: CI/CD Integration)
- **Unit & Integration Testing** (XUnit)
- **Git for Version Control**

## 🔥 Features
- **User Authentication & Authorization** (Role-Based Access Control)
- **Leave Request & Approval Workflow**
- **Employee Management**
- **Blazor-based Frontend UI**
- **Secure API Endpoints**
- **Logging & Error Handling**
- **MediatR & CQRS**

##  🏗 Architecture

This project follows the **Clean Architecture** principles, ensuring separation of concerns and maintainability.

<img alt="clean_diagram.jpg" style="width:250px" src="https://github.com/vik37/HR.LeaveManagement/blob/feature/clean_diagram.jpg?raw=true" data-hpc="true" class="Box-sc-g0xbh4-0 fzFXnm">

**src** :open_file_folder:         
 ```` API ````:open_file_folder:          
>    | *HR.Leave.Management.API (.NET Web API)* |
>    |------------------------------------------|
         
```` Core ````:open_file_folder:               
>    | *HR.Leave.Management.Domain (Domain Entities)* |
>    |------------------------------------------------|
>         
>    | *HR.Leave.Management.Application (Business Logic)* |
>    |----------------------------------------------------|
              
```` Infrastructure (Third-Party Services) ````:open_file_folder:                                  
>    | *HR.Leave.Management.Identity (User Security)* |
>    |------------------------------------------------|
>                 
>    | *HR.Leave.Management.Infrastructure (Logging Service, Email Service)* |
>    |-----------------------------------------------------------------------|
>             
>    | *HR.Leave.Management.Persistence (Database Access, Repositories, E.F. Migrations)* |
>    |------------------------------------------------------------------------------------|
          
```` UI ````:open_file_folder:                                  
>    | *HR.Leave.Management.UI (.NET Blazor WebAssembly)* |
>    |----------------------------------------------------|
                 
**Test**:open_file_folder:               
```` 1. Unit Tests ````:file_folder:                
```` 2. Integration Tests ````:file_folder:

## 🛠 Installation & Setup
### Prerequisites
- .NET SDK installed
- MSSQL Server running
- Git installed

### Steps to Run Locally
1. **Clone the repository:**
   ```sh
   git clone https://github.com/vik37/HR.LeaveManagement.git
   cd HR.LeaveManagement
   ```
2. **Set up the database**
   - This project uses **User Secrets** to store sensitive configuration values like database connection strings.
   - Instead of modifying `appsettings.json` directly, use the following command to add your connection string:
     ```sh
     dotnet user-secrets set "HrDatabaseConnectionString" "YourDatabaseConnectionString"
     ```
   - In `appsettings.json`, the connection string is referenced as:
     ```json
     "ConnectionStrings": {
       "HrDatabaseConnectionString": "DatabaseSecrets"
     }
     ```
4. **Run the project:**
   ```sh
   dotnet run
   ```
5. **Access the app on:** `https://localhost:7217, http://localhost:5252`.

## ✅ Unit Testing
To run the unit tests:
```sh
dotnet test
```

## 📜 License
This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.

## 🤝 Contributing
Feel free to submit issues or pull requests to help improve the project!

## 📩 Contact
For any inquiries, you can reach me at: **vik.zafirovski@gmail.com**

---
⚡ *This project is actively maintained and improved. Any feedback is welcome!*


--------------------------------
====================
