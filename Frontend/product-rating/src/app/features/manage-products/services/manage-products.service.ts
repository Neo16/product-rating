import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ProductManageHeaderData } from 'src/app/models/products/ProductHeaderData';
import { ManageProductFilterData } from 'src/app/models/products/ManageProductFilterData';
import { CreateEditProductData } from 'src/app/models/products/CreateEditProductData';
import { OfferHeaderData } from 'src/app/models/products/OfferHeaderData';
import { CreateEditOfferData } from 'src/app/models/products/CreateEditOfferData';
import { environment } from 'src/environments/environment';

@Injectable()
export class ManageProductsService {
    private BASE_URL = environment.apiBaseUrl;

    constructor(private http: HttpClient) { }

    getProducts(filter: ManageProductFilterData, pagination: PaginationParams): Observable<ProductManageHeaderData[]> {
        const url = `${this.BASE_URL}/manage-products/find`;

        // pagination query parameters 
        let queryParams = new HttpParams();
        queryParams = queryParams.append('length', pagination.length.toString());
        queryParams = queryParams.append('start', pagination.start.toString());

        return this.http.post<ProductManageHeaderData[]>(url, filter, { params: queryParams });
    }

    getProduct(productId: string): Observable<CreateEditProductData> {
        const url = `${this.BASE_URL}/manage-products/${productId}/for-update`;
        return this.http.get<CreateEditProductData>(url);
    }

    createProduct(product: CreateEditProductData): Observable<any> {
        var toUpload = { ...product };
        toUpload.thumbnailPicture.data = null;

        const url = `${this.BASE_URL}/manage-products`;
        return this.http.post<any>(url, JSON.stringify(toUpload));
    }

    updateProduct(productId: string, product: CreateEditProductData): Observable<any> {
        const url = `${this.BASE_URL}/manage-products/${productId}`;
        return this.http.put<any>(url, JSON.stringify(product));
    }

    deleteProduct(productId: string): Observable<any> {
        const url = `${this.BASE_URL}/manage-products/${productId}`;
        return this.http.delete<any>(url);
    }

    getOffer(productId: string): Observable<OfferHeaderData>{
        const url = `${this.BASE_URL}/manage-products/${productId}/offer`;
        return this.http.get<any>(url);
    }

    addOffer(productId: string, data: CreateEditOfferData): Observable<any>{
        const url = `${this.BASE_URL}/manage-products/${productId}/offer`;
        return this.http.post<CreateEditOfferData>(url, JSON.stringify(data));
    }

    deletetOffer(productId: string): Observable<any>{
        const url = `${this.BASE_URL}/manage-products/${productId}/offer`;
        return this.http.delete<any>(url);
    }




}