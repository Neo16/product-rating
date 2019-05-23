import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CategoryHeader } from 'src/app/models/search/CategoryHeader';
import { SearchCategoryAttributeData } from 'src/app/models/categories/search/SearchCategoryAttributeData';

@Injectable()
export class SearchHelperService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  getSubCategoriesOf(categoryId: string): Observable<CategoryHeader[]> {
    const url = `${this.BASE_URL}/categories/${categoryId}/subcategories`; 
    return this.http.get<CategoryHeader[]>(url);
  }

  getAttributesOf(categoryId: string): Observable<SearchCategoryAttributeData[]> {
    const url = `${this.BASE_URL}/categories/${categoryId}/attributes`; 
    return this.http.get<SearchCategoryAttributeData[]>(url);
  }
}