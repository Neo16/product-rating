import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProfileData } from 'src/app/models/profile/ProfileData';
import { EditProfileData } from 'src/app/models/profile/EditProdileData';

@Injectable()
export class ProfileService {
    private BASE_URL = 'https://localhost:44394';

    constructor(private http: HttpClient) { }

    getMyProfile(): Observable<ProfileData> {
        const url = `${this.BASE_URL}/profile`;
        return this.http.get<ProfileData>(url);
    }

    getProfileById(userId: string): Observable<ProfileData> {
        const url = `${this.BASE_URL}/profile/${userId}`;
        return this.http.get<ProfileData>(url, );
    }

    editProfile(profile: EditProfileData): Observable<any> {
        const url = `${this.BASE_URL}/profile/update-profile`;
        return this.http.post<ProfileData>(url, JSON.stringify(profile));
    }
}