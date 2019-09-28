import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/User';
import { Observable } from 'rxjs';
import { LoginResultData } from 'src/app/models/LoginResultData';
import { RegisterData } from 'src/app/models/RegisterData';

@Injectable()
export class AccountService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  getToken(): string {
    return localStorage.getItem('productrating-token');
  }

  logIn(username: string, password: string): Observable<LoginResultData> {  
    const url = `${this.BASE_URL}/account/login`;
    return this.http.post<LoginResultData>(url, JSON.stringify({username, password}));
  }

  register(data: RegisterData): Observable<any> {  
    const url = `${this.BASE_URL}/account/register`;
    return this.http.post<any>(url, JSON.stringify(data));
  }

}