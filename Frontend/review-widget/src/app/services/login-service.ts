import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResultData } from '../models/LoginResultData';

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    private BASE_URL = 'https://localhost:44394';

    constructor(private http:HttpClient){}

    logIn(username: string, password: string): Observable<LoginResultData> {  
        const url = `${this.BASE_URL}/account/login`;
        return this.http.post<LoginResultData>(url, JSON.stringify({username, password}));
    }
    
}