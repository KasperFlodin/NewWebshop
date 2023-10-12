import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../_models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSubject: BehaviorSubject<User>;  // pushes current value (of the User) to subscribers
  public currentUser: Observable<User>                // exposes an observable so any component can subscribe to be notified of user login, log out, token refresh and edit
                                                      

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser') as string));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User{
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    let authenticateUrl = `${environment.apiUrl}User/authenticate`;
    return this.http.post<User>(authenticateUrl, {"email": email, "password": password})
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        this.currentUserValue.role;
        console.log(this.currentUserValue.role);
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    // reset CurrentUserSubject, by fetching the value in localStorage, which is null at this point
    this.currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser') as string));
    // reset CurrentUser to the resat UserSubject, as an obserable
    this.currentUser = this.currentUserSubject.asObservable();
  }

  register(user: User) {
    return this.http.post(`${environment.apiUrl}User/register`, user);
  }

  update(id: any, user: User) {
    return this.http.put(`${environment.apiUrl}User/${id}`, user)
        .pipe(map(x => {
            // update stored user if the logged in user updated their own record
            if (user.id == this.currentUserValue?.id) {
                // update local storage
                const currentUser = { ...this.currentUserValue, ...user };
                localStorage.setItem('currentUser', JSON.stringify(user));

                // publish updated user to subscribers
                this.currentUserSubject.next(user);
            }
            return x;
        }));
  }

  delete(id: number) {
    return this.http.delete(`${environment.apiUrl}user/${id}`)
        .pipe(map(x => {
            // auto logout if the logged in user deleted their own record
            if (id == this.currentUserValue?.id) {
                this.logout();
            }
            return x;
        }));
  }

}
