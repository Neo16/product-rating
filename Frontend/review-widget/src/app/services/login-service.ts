import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResultData } from '../models/LoginResultData';

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    private BASE_URL = 'https://localhost:44394';
    private API_KEY = '';

    constructor(private http: HttpClient) { }

    setApiKey(key: string) {
        this.API_KEY = key;
    }

    logIn(username: string, password: string): Observable<LoginResultData> {
        const url = `${this.BASE_URL}/account/login?key=${this.API_KEY}`;
        return this.http.post<LoginResultData>(url, JSON.stringify({ username, password }));
    }

}