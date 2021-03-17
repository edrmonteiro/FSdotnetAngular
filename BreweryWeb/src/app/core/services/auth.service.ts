import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'src/app/user/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  baseURL = 'https://localhost:5001/api/v1/account/'
  jwtHelper = new JwtHelperService()
  decodedToken: any;
  userName = '';

  login(user: User): Observable<User> {
    return this.http.post<User>(this.baseURL+"login", user)
    .pipe(
      map((response: any) => {
        console.log(response);
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('email', user.email);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          return user;
        }        
      })
    );
  }

  register(user: User): Observable<User> {
    //console.log(user);
    return this.http.post<User>(this.baseURL+"register", user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
