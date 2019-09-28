import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { UserManageHeaderData } from 'src/app/models/users/UserManageHeaderData';
import { ManageUserFilterData } from 'src/app/models/users/ManageUserFilterData';

@Injectable()
export class ManageUsersService {
    private BASE_URL = 'https://localhost:44394';

    constructor(private http: HttpClient) { }

    getUsers(filter: ManageUserFilterData, pagination: PaginationParams): Observable<UserManageHeaderData[]> {
        const url = `${this.BASE_URL}/manage-users/list`;

        // pagination query parameters 
        let queryParams = new HttpParams();
        if (pagination.length != null && pagination.start != null) {
            queryParams = queryParams.append('length', pagination.length.toString());
            queryParams = queryParams.append('start', pagination.start.toString());
        }
        return this.http.post<UserManageHeaderData[]>(url, filter, { params: queryParams });
    }

    lockoutUser(UserId: string): Observable<any> {
        const url = `${this.BASE_URL}/manage-users/${UserId}/lockout`;
        return this.http.post<any>(url, null);
    }

    admitUser(UserId: string): Observable<any> {
        const url = `${this.BASE_URL}/manage-users/${UserId}/admit`;
        return this.http.post<any>(url, null);
    }
}