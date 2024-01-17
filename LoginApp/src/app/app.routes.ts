import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HomeComponent } from './components/home/home.component';
import { authGuard } from './services/auth.guard';
import { RegisterListComponent } from './components/register-list/register-list.component';
import { adminGuard } from './services/admin.guard';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register/edit/:id',
        component: RegisterComponent,
      },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'registerList',
        component: RegisterListComponent,
        canActivate: [authGuard, adminGuard]
    },
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [authGuard]
    },
    {
        path: '**',
        component: PageNotFoundComponent
    }
];
