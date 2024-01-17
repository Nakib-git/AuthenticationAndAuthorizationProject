import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Login } from '../../model/user/Login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  login: Login;
  constructor(private loginAuth: AuthService, private router: Router) {
    this.login = new Login();
  }
  ngOnInit(): void {
  }
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required,
    Validators.minLength(4), Validators.maxLength(6)])
  });
  get Password(): FormControl {
    return this.loginForm.get('password') as FormControl;
  }
  get Email(): FormControl {
    return this.loginForm.get('email') as FormControl;
  }
  isUserValid: boolean = false;
  loginSubmited() {
    this.login.email = this.Email.value;
    this.login.password = this.Password.value;

    this.loginAuth
      .loginUser(this.login)
      .subscribe(res => {
        if (res.status === 401) {
          this.isUserValid = false;
          alert('Login unsuccessful.');
        } else {
          this.isUserValid = true;
          this.loginAuth.setToken(res.data.token);
          this.router.navigateByUrl("/home")
        }
      });
  }
}
