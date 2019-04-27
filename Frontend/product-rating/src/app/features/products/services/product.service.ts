import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { Observable } from 'rxjs';
import { SearchParams } from 'src/app/models/SearchParams';
import { SearchResult } from 'src/app/models/SearchResult';
import { PaginationParams } from 'src/app/models/PaginationParams';

@Injectable()
export class ProductService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  searchProducts(searchParams: SearchParams | {}, paginationParams: PaginationParams): Observable<SearchResult> {
    const url = `${this.BASE_URL}/products/find`;
    if (searchParams == null){
      searchParams = {}
    }

    // pagination query parameters 
    let queryParams = new HttpParams();   
    queryParams = queryParams.append('length', paginationParams.length.toString());
    queryParams = queryParams.append('start', paginationParams.start.toString());

    return this.http.post<SearchResult>(url, searchParams, {params: queryParams});
  }
}