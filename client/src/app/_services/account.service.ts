import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUel = "https://localhost:7197/api/";
  private CurrentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.CurrentUserSource.asObservable();
  constructor(private http: HttpClient) { }
  login(model: any) {
    return this.http.post<User>(this.baseUel + 'account/login', model).pipe(map((response: User) => {
      const user = response;
      if (user) {
        localStorage.setItem("user", JSON.stringify(user));
        this.CurrentUserSource.next(user);
      }
    }));
  }//login
  Register(model: any) {
    return this.http.post<User>(this.baseUel + 'account/register', model).pipe(map((user: User) => {
      if (user) {
        localStorage.setItem("user", JSON.stringify(user));
        this.CurrentUserSource.next(user);
      }//if
     // return user;
    }))
  }//register
  SetCurrentUser(user: User) {
    this.CurrentUserSource.next(user);
  }
  logout() {
    localStorage.removeItem("user");
    this.CurrentUserSource.next(null);
  }//logout
}
