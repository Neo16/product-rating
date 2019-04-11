import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { Observable } from 'rxjs';
import { SearchParams } from 'src/app/models/SearchParams';
import { SearchResult } from 'src/app/models/SearchResult';

@Injectable()
export class ProductService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  searchProducts(params: SearchParams | {} ): Observable<SearchResult> {
    const url = `${this.BASE_URL}/products/find`;
    if (params == null){
      params = {}
    }
    return this.http.post<SearchResult>(url, params);
  }
}