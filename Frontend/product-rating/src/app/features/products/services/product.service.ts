import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProductCellData } from 'src/app/models/products/ProductCellData';
import { Observable } from 'rxjs';
import { SearchParams } from 'src/app/models/search/SearchParams';
import { SearchResult } from 'src/app/models/search/SearchResult';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ProductDetailsData } from 'src/app/models/products/ProductDetailsData';
import { OfferHeaderData } from 'src/app/models/products/OfferHeaderData';

@Injectable()
export class ProductService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  getProductDetails(productId: string): Observable<ProductDetailsData> {    
    const url = `${this.BASE_URL}/products/${productId}`;
    return this.http.get<ProductDetailsData>(url);
  }
  
  searchProducts(
    searchParams: SearchParams | {},
    paginationParams: PaginationParams
    ): Observable<SearchResult> {      
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

  getOffers(productId: string): Observable<OfferHeaderData[]>{    
    const url = `${this.BASE_URL}/products/${productId}/list-offes`;
    return this.http.get<OfferHeaderData[]>(url);
  }
}