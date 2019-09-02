import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ManageBrandFilterData } from 'src/app/models/brands/ManageBrandFilterData';
import { BrandManageHeaderData } from 'src/app/models/brands/BrandManageHeaderData';
import { CreateEditBrandData } from 'src/app/models/brands/CreateEditBrandData';

@Injectable()
export class ManageBrandsService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) { }

  getBrands(filter: ManageBrandFilterData, pagination: PaginationParams): Observable<BrandManageHeaderData[]> {

    const url = `${this.BASE_URL}/manage-brands/list`;

    // pagination query parameters 
    let queryParams = new HttpParams();
    if (pagination.length != null && pagination.start != null) {
      queryParams = queryParams.append('length', pagination.length.toString());
      queryParams = queryParams.append('start', pagination.start.toString());
    }

    return this.http.post<BrandManageHeaderData[]>(url, filter, { params: queryParams });
  }

  getBrand(brandId: string): Observable<CreateEditBrandData> {
    const url = `${this.BASE_URL}/manage-brands/get-for-update/${brandId}`;
    return this.http.get<CreateEditBrandData>(url);
  }

  createBrand(brand: CreateEditBrandData): Observable<any> {
    const url = `${this.BASE_URL}/manage-brands/create`;
    return this.http.post<any>(url, JSON.stringify(brand));
  }

  updateBrand(brandId: string, brand: CreateEditBrandData): Observable<any> {
    const url = `${this.BASE_URL}/manage-brands/${brandId}/update`;
    return this.http.put<any>(url, JSON.stringify(brand));
  }

  deleteBrand(brandId: string): Observable<any> {
    const url = `${this.BASE_URL}/manage-brands/${brandId}/delete`;
    return this.http.delete<any>(url);
  }
}