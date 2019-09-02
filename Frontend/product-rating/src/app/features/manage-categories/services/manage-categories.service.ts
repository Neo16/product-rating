import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateEditCategoryData } from 'src/app/models/categories/CreateEditCategoryData';
import { CreateEditCategoryAttributeValueData } from 'src/app/models/categories/CreateEditCategoryAttributeValueData';
import { AttributeType } from 'src/app/models/categories/AttributeType';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ManageCategoryFilterData } from 'src/app/models/categories/ManageCategoryFilterData';
import { CategoryManageHeaderData } from 'src/app/models/categories/CategoryManageHeaderData';

@Injectable()
export class ManageCategoriesService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) { }

  getCategories(filter: ManageCategoryFilterData, pagination: PaginationParams): Observable<CategoryManageHeaderData[]> {
    const url = `${this.BASE_URL}/manage-categories/list`;

    // pagination query parameters 
    let queryParams = new HttpParams();
    if (pagination.length != null && pagination.start != null){
      queryParams = queryParams.append('length', pagination.length.toString());
      queryParams = queryParams.append('start', pagination.start.toString());
    } 

    return this.http.post<CategoryManageHeaderData[]>(url, filter, { params: queryParams });
  }

  getCategory(categoryId: string): Observable<CreateEditCategoryData> {
    const url = `${this.BASE_URL}/manage-categories/get-for-update/${categoryId}`;
    return this.http.get<CreateEditCategoryData>(url);
  }

  createCategory(category: CreateEditCategoryData): Observable<any> {
    const url = `${this.BASE_URL}/manage-categories/create`;
    return this.http.post<any>(url, JSON.stringify(category));
  }

  updateCategory(categoryId: string, category: CreateEditCategoryData): Observable<any> {
    const url = `${this.BASE_URL}/manage-categories/${categoryId}/update`;  
    return this.http.put<any>(url, JSON.stringify(category));
  }

  deleteCategory(categoryId:string) : Observable<any>{
    const url = `${this.BASE_URL}/manage-categories/${categoryId}/delete`;  
    return this.http.delete<any>(url);
  }
}