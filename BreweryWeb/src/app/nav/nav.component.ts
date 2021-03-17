import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  status: string;
  user: string;
  constructor(public router: Router,
    private toastr: ToastrService,
    private authService: AuthService,) { }

  ngOnInit() {
    status = "logged";
  }
  loggedIn(){
    this.user = localStorage.getItem('email');
    return this.authService.loggedIn();    
  }
  
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    this.toastr.show('Log out');
    this.router.navigate(['/user/login']);
  }

}
