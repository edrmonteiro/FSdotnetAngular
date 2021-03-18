import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/core/services/user.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string;
  password: string;
  constructor(public router: Router,
              public fb: FormBuilder,
              private toastr: ToastrService,
              private authService: UserService,
              ) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null){
      this.router.navigate(['home'])
    }
  }
  login(): void {
    var user:User = { 
      email: this.email, 
      password: this.password, 
      admin: false
    };
    this.authService.login(user).subscribe(
      (result) => {
        //console.log(result);
        this.router.navigate(['']);
        this.toastr.success('Welcome ' + this.email);
      },
      error => {
        console.log(error);
        this.toastr.error("user or password wrong");
      });
  }
}
