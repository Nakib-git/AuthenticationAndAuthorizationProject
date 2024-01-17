import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { UserToken } from '../../model/user/UserToken';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  loginUser: UserToken = new UserToken();
  constructor(private authService: AuthService, private router: Router) {
  }
  ngOnInit(): void {
    this.getLoginUser();
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('user_info');
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }

  getLoginUser() {
    if (typeof localStorage !== 'undefined') {
      const user = localStorage.getItem("user_info");
      if (user) {
        this.loginUser = JSON.parse(user) as unknown as UserToken;
      }
    }
  }

}
