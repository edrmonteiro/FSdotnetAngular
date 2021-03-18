import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  isLogged: boolean;

  constructor(private authService: UserService) { }

  ngOnInit(): void {

  }

}
