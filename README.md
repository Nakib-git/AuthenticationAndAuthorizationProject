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
clone this project and run below command in root directory of project. <br/>
SSH `*` <br/>
HTTPS `*`

**How to run backend**

1. In repository go to this directory `~/src/AuthenticationAndAuthorizationProject/Register.WebApi`.
2. open visual studio with `Register.sln`.
3. set startup project `Register.WebApi`.
4. open Package Manager Console and select default porject `Register.Infrastructure`.
5. Change connection string in appsettings.json.
6. run this command `update-database`.
7. run the project and remiemebr the `https://localhost:yourport`.

**How to run Front-End**

1. Open editor go to `src/app/services/auth.service`
2. update your port `baseUrl: 'https://localhost:yourport/api/'`
3. Install latest node and angular
4. Now use `npm  install`
5. Now use `ng serve`

# Author

Md. Shafiqur Rahman <br/>
Software Engineer


