import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiURL = environment.apiUrl + 'user';

  constructor(private http: HttpClient) { }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.apiURL);
  }

  getById(userId: number): Observable<User> {
    return this.http.get<User>(this.apiURL + '/'+ 'user' + '/' + userId);
  }

  create(user: User): Observable<User> {
    return this.http.post<User>(this.apiURL, user);
  }

  // update(userId: number, user: User): Observable<User> {
  //   return this.http.put<User>(`${this.apiURL}/${user.id}`, user);
  // }

  update(userId: number, user: User): Observable<User> {
    return this.http.put<User>(this.apiURL + '/' + user.id, user);
  }

  delete(userId: number | undefined): Observable<User> {
    return this.http.delete<User>(`${this.apiURL}/${userId}`);
  }
}
