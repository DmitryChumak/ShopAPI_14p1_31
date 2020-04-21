import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
  })
  export class AuthService {
    baseUrl = environment.apiUrl + 'auth/';
    jwtHelper = new JwtHelperService();
    decodedToken: any;
    currentUser: User;

    constructor(private http: HttpClient) {}

    login(model: any) {
      return this.http.post(this.baseUrl + 'authenticate', model).pipe(
        map((response: any) => {
          const user = response.data;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(response.data));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user;
          }
        })
      );
    }

    register(user: User) {
      return this.http.post(this.baseUrl + 'register', user);
    }

    loggedIn() {
      const token = localStorage.getItem('token');
      if (token !== 'undefined') {
        return !this.jwtHelper.isTokenExpired(token);
      } else {return false; }
    }

    roleMatch(allowedRoles): boolean {
      let isMatch = false;
      const userRoles = this.decodedToken.role as Array<string>;
      allowedRoles.forEach(element => {
        if (userRoles.includes(element)) {
          isMatch = true;
          return;
        }
      });
      return isMatch;
    }
  }
