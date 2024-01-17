# Authentication And Authorization Project for NybSis 

**Project feature**
1. Login and registration page design
2. After login make a welcome page with logged in user name
3. Create a display page where only the admin user can view the list of registered user and can remove/modify user
4. Integrate logout functionality

   
**Stack reference:**

- Asp.Net core `8`
- Angular `17`
- Bootstrap `5`
- Node version `20.6.0`
- SQL Server

# Project startup instruction
Clone this project and run below command in root directory of project. <br/>
HTTPS `https://github.com/Nakib-git/AuthenticationAndAuthorizationProject.git`

**How to run backend**

1. In repository go to this directory `~/src/AuthenticationAndAuthorizationProject/Register.WebApi`.
2. Open visual studio with `Register.sln`.
3. Set startup project `Register.WebApi`.
4. Open Package Manager Console and select default porject `Register.Infrastructure`.
5. Change connection string in appsettings.json.
6. Run this command `update-database`.
7. Run the project and remiemebr the `https://localhost:yourport`.

**How to run Front-End**

1. Open editor go to `src/app/services/auth.service`.
2. Update your port `baseServerUrl: 'https://localhost:yourport/api/'`.
3. Install latest node and angular.
4. Use `npm  install`.
5. Now use `ng serve`.

# Author

Md. Shafiqur Rahman <br/>
Software Engineer


