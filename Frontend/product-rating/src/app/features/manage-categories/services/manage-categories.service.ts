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

  getCategories(filter: ManageCategoryFilterData, pagination: PaginationParams): Observable<CategoryManageHeaderData[]>{
    const url = `${this.BASE_URL}/manage-categories/list`;

        // pagination query parameters 
        let queryParams = new HttpParams();   
        queryParams = queryParams.append('length', pagination.length.toString());
        queryParams = queryParams.append('start', pagination.start.toString());
    
        return this.http.post<CategoryManageHeaderData[]>(url, filter, {params: queryParams});
  }

  createCategory(category: CreateEditCategoryData): Observable<any> {
    const url = `${this.BASE_URL}/manage-categories/create`;

    var categoryToPost: any = JSON.parse(JSON.stringify(category));      

    categoryToPost.attributes.forEach(attr => {
      attr.values = attr.values.map(v => {        
        var newValue: any = {         
          valueId: v.valueId
        }
        if (attr.type == AttributeType.Int) {
          newValue.intValue = v.intValue;
        }
        if (attr.type == AttributeType.String) {
          newValue.stringValue = v.stringValue;
        }     
        return newValue;      
      })
    });

    return this.http.post<any>(url, JSON.stringify(categoryToPost));
  }
}