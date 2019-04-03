import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { Observable } from 'rxjs';
import { SearchParams } from 'src/app/models/SearchParams';

@Injectable()
export class ProductService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  searchProducts(params: SearchParams | {} ): Observable<ProductCellData[]> {
    const url = `${this.BASE_URL}/products/find`;
    return this.http.post<ProductCellData[]>(url, params);
  }
}