import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../model/user/User';
import { Login } from '../model/user/Login';
import { BehaviorSubject, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Response } from '../model/user/Response';
import { UserToken } from '../model/user/UserToken';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) {
  }
  public get requestHeaders(): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.getToken()}`);
    return { headers };
  }
  baseServerUrl = 'https://localhost:7249/api/';
  jwtHelperService = new JwtHelperService();
  // currentUser: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  createRegisterUser(user: User) {
    return this.http.post<Response>(this.baseServerUrl + 'User/register-user', user, this.requestHeaders)
  }
  ureateRegisterUser(user: User) {
    return this.http.put<Response>(this.baseServerUrl + 'User/update-user', user, this.requestHeaders)
  }

  getAllUser() {
    return this.http.get<Response>(this.baseServerUrl + 'User/all-users', this.requestHeaders)
  }

  getUserById(id: number) {
    return this.http.get<Response>(`${this.baseServerUrl}User/get-by-id/${id}`, this.requestHeaders)
  }
  deleteUser(id: number) {
    return this.http.delete<Response>(`${this.baseServerUrl}User/delete/${id}`, this.requestHeaders)
  }

  loginUser(loginInfo: Login) {
    return this.http.post<Response>(this.baseServerUrl + 'User/login', loginInfo, this.requestHeaders)
  }

  setToken(token: string) {
    localStorage.setItem("access_token", token);
    this.loadCurrentUser();
  }
  getToken() {
    if (typeof localStorage !== 'undefined') {
      const token = localStorage.getItem("access_token");
      return token;
    } else {
      return '';
    }
  }

  loadCurrentUser() {
    const token = localStorage.getItem("access_token");
    const user_info = token != null ? this.jwtHelperService.decodeToken(token) : '';
    localStorage.setItem("user_info", JSON.stringify(user_info));

  }

  isLoggedin(): boolean {
    if (typeof localStorage !== 'undefined') {
      var token = localStorage.getItem("access_token");
      if (token) {
        return true;
      }
      return false;
    } else {
      return false;
    }

  }

  isAdmin(): boolean {
    if (typeof localStorage !== 'undefined') {
      const userToken = localStorage.getItem("user_info");
      const user = JSON.parse(userToken ?? '') as unknown as UserToken
      if (user.role === 'Admin') {
        return true;
      }
    } else {
      return false;
    }
    return false;
  }

  getAllRoles() {
    return this.http.get<Response>(this.baseServerUrl + 'Role/all-roles', this.requestHeaders)
  }
}
