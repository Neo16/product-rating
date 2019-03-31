import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/User';
import { Observable } from 'rxjs';
import { tap, map} from 'rxjs/operators';


@Injectable()
export class AccountService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  getToken(): string {
    return localStorage.getItem('token');
  }

  logIn(username: string, password: string): Observable<any> {
    const url = `${this.BASE_URL}/account/login`;
    return this.http.post<User>(url, JSON.stringify({username, password}));
  }
}