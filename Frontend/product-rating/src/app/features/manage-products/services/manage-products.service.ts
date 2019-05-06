import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AttributeType } from 'src/app/models/categories/AttributeType';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ProductManageHeaderData } from 'src/app/models/products/ProductHeaderData';
import { ManageProductFilterData } from 'src/app/models/products/ManageProductFilterData';


@Injectable()
export class ManageProductsService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) { }

  getProducts(filter: ManageProductFilterData, pagination: PaginationParams): Observable<ProductManageHeaderData[]> {
    const url = `${this.BASE_URL}/manage-products/list`;
    
    // pagination query parameters 
    let queryParams = new HttpParams();
    queryParams = queryParams.append('length', pagination.length.toString());
    queryParams = queryParams.append('start', pagination.start.toString());

    return this.http.post<ProductManageHeaderData[]>(url, filter, { params: queryParams });
  }

//   getProduct(categoryId: string): Observable<CreateEditProductData> {
//     const url = `${this.BASE_URL}/manage-products/get-for-update/${categoryId}`;
//     return this.http.get<CreateEditProductData>(url);
//   }

//   createProduct(category: CreateEditProductData): Observable<any> {
//     const url = `${this.BASE_URL}/manage-products/create`;
//     return this.http.post<any>(url, JSON.stringify(category));
//   }

//   updateProduct(categoryId: string, category: CreateEditProductData): Observable<any> {
//     const url = `${this.BASE_URL}/manage-products/${categoryId}/update`;  
//     return this.http.put<any>(url, JSON.stringify(category));
//   }
}