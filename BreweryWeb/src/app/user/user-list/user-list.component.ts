import { Component, OnChanges, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/core/services/user.service';
import { User } from 'src/app/shared/models/user';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit, OnChanges {
  users: User[];

  constructor(public router: Router,
              private toastr: ToastrService,
              private userService: UserService,
              ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  ngOnChanges(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.userService.getAllUsers().subscribe(
      (result) => {
        this.users = result;
        //this.router.navigate(['']);
        //this.toastr.success('Welcome ' + this.email);
      },
      error => {
        console.log(error);
        this.toastr.error("user or password wrong");
      });
  }

  remove(user: User):void {  
    this.userService.removeUser(user).subscribe(
      (result) => {
        this.getUsers();
      },
      error => {
        console.log(error);
        this.toastr.error("Error");
      });

  }

  turnAdminOn(user: User):void {
    this.userService.turnAdminOn(user).subscribe(
      (result) => {
        this.getUsers();
      },
      error => {
        console.log(error);
        this.toastr.error("Error");
      });

  }

  turnAdminOff(user: User):void {
    this.userService.turnAdminOff(user).subscribe(
      (result) => {
        this.getUsers();
      },
      error => {
        console.log(error);
        this.toastr.error("Error");
      });
  }

}
