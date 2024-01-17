import { Component, OnInit } from '@angular/core';
import { User } from '../../model/user/User';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './register-list.component.html',
  styleUrl: './register-list.component.css'
})
export class RegisterListComponent implements OnInit {
  userList: User[] = [];
  dispalyMsg = '';
  constructor(private authService: AuthService, private router: Router) {
    this.authService.getAllUser().subscribe(res => {
      this.userList = res.data as User[];
    })
  }
  ngOnInit(): void {

  }

  editUser(userId: number): void {
    // Implement the logic for editing a user (navigate to edit page or show modal)
    if (userId > 0) {
      this.router.navigateByUrl(`register/edit/${userId}`)
    }
  }

  deleteUser(userId: number): void {
    if (userId > 0) {
      this.authService.deleteUser(userId).subscribe(res => {
        if (res.data == true) {
          this.dispalyMsg = 'Deleted successfully'
        } else {
          this.dispalyMsg = 'Operation faild'
        }
      })
    } else {
      alert("User Id is not valid.")
    }

  }

}
