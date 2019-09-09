import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ScoreService{
    private BASE_URL = 'https://localhost:44394';

    constructor(private http: HttpClient) {}

    getProductScore(productId: string){    
        const url = `${this.BASE_URL}/products/${productId}/score`;
        return this.http.get<number>(url);
     }
}