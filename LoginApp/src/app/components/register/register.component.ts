import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { User } from '../../model/user/User';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from '../../model/role/Role';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  user: User = new User();
  dispalyMsg = '';
  isAccountCreated = false;
  registerForm = new FormGroup({
    id: new FormControl(null),
    userName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl('', [Validators.maxLength(13)]),
    password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(6)]),
    repeatPassword: new FormControl(""),
    roleId: new FormControl(null)
  });
  userId: number | null = null;
  roles: Role[] = []
  constructor(private authService: AuthService, private route: ActivatedRoute, public builder: FormBuilder, private router: Router) {

  }
  ngOnInit() {
    this.authService.getAllRoles().subscribe(res => {
      if (res.status === 200) {
        this.roles = res.data as Role[];
      }
    });
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id != null) {
        this.userId = parseInt(id, 10);
        this.authService.getUserById(parseInt(id, 10)).subscribe(res => {
          if (res != null) {
            this.user = res.data as User;
          }
          this.createForm();
        })
      }
    })
  }

  createForm(data: User | null = null): void {
    if (data) {
      this.user = data;
    }
    // @ts-ignore
    this.registerForm = this.builder.group({
      id: this.user.id,
      userName: [this.user.userName ?? '', [Validators.required]],
      email: [this.user.email ?? '', [Validators.required, Validators.email]],
      phoneNumber: [this.user.phoneNumber ?? '', [Validators.maxLength(13)]],
      password: [this.user.password ?? '', [Validators.required, Validators.minLength(4), Validators.maxLength(6)]],
      repeatPassword: ['',],
      roleId: [this.user.roleId],
    });
  }

  get Id(): FormControl {
    return this.registerForm.get('id') as FormControl;
  }
  get UserName(): FormControl {
    return this.registerForm.get('userName') as FormControl;
  }
  get Password(): FormControl {
    return this.registerForm.get('password') as FormControl;
  }
  get RepeatPassword(): FormControl {
    return this.registerForm.get('repeatPassword') as FormControl;
  }
  get Email(): FormControl {
    return this.registerForm.get('email') as FormControl;
  }
  get Phonenumber(): FormControl {
    return this.registerForm.get('phoneNumber') as FormControl;
  }
  get Role(): FormControl {
    return this.registerForm.get('roleId') as FormControl;
  }
  registerSumbit() {
    const id = this.registerForm.get('id') as FormControl
    if (id.value != null && id.value > 0) {
      this.registerUpdate()
    } else {
      this.registerAdd();
    }
  }
  registerAdd() {
    if (this.Password.value == this.RepeatPassword.value) {
      this.user.email = this.Email.value;
      this.user.userName = this.UserName.value;
      this.user.password = this.Password.value;
      this.user.phoneNumber = this.Phonenumber.value;
      this.user.roleId = this.Role.value;

      this.authService.createRegisterUser(this.user).subscribe(res => {
        if (res.status == 200) {
          this.dispalyMsg = 'Account created successfully';
          this.isAccountCreated = true;
          this.router.navigateByUrl('/login')
        } else if (res.status == 401) {
          this.dispalyMsg = 'Account already exist.Try another email.';
          this.isAccountCreated = false;
        } else {
          this.dispalyMsg = 'Something went worng.';
          this.isAccountCreated = false;
        }
      })

    } else {
      this.dispalyMsg = 'Password not matched!'
      this.isAccountCreated = false;
    }
  }

  registerUpdate() {
    this.user.email = this.Email.value;
    this.user.userName = this.UserName.value;
    this.user.password = this.Password.value;
    this.user.phoneNumber = this.Phonenumber.value;
    this.user.roleId = this.Role.value;

    this.authService.ureateRegisterUser(this.user).subscribe(res => {
      if (res.status == 200) {
        this.dispalyMsg = 'Account update successfully';
        this.isAccountCreated = true;
        this.router.navigateByUrl('/registerList')
      } else if (res.status == 401) {
        this.dispalyMsg = 'Account already exist.Try another email.';
        this.isAccountCreated = false;
      } else {
        this.dispalyMsg = 'Something went worng.';
        this.isAccountCreated = false;
      }
    })

  }
}
