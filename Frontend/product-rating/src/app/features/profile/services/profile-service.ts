import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProfileData } from 'src/app/models/profile/ProfileData';
import { EditProfileData } from 'src/app/models/profile/EditProdileData';
import { ChangePasswordData } from 'src/app/models/ChangePasswordData';
import { RequireSubscriptionData } from 'src/app/models/profile/RequireSubscriptionData';
import { SubscriptionData } from 'src/app/models/profile/SubscriptionData';
import { TextReviewWithProductInfoData } from 'src/app/models/profile/TextReviewWithProductInfoData';

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
        return this.http.get<ProfileData>(url);
    }

    getReviewsMadeByUser(userId: string): Observable<TextReviewWithProductInfoData[]>{
        const url = `${this.BASE_URL}/profile/${userId}/reviews`;
        return this.http.get<TextReviewWithProductInfoData[]>(url);
    }

    editProfile(profile: EditProfileData): Observable<any> {
        const url = `${this.BASE_URL}/profile/update-profile`;
        return this.http.put<any>(url, JSON.stringify(profile));
    }

    changePassword(data: ChangePasswordData): Observable<any> {
        const url = `${this.BASE_URL}/account/change-password`;
        return this.http.post<any>(url, JSON.stringify(data));
    }

    getSubscriptions(): Observable<any> {
        const url = `${this.BASE_URL}/profile/subscriptions`;
        return this.http.get<any>(url);
    }
    requireSubscription(request: RequireSubscriptionData): Observable<SubscriptionData[]> {
        const url = `${this.BASE_URL}/profile/require-subscription`;
        return this.http.post<any>(url, JSON.stringify(request));
    }
    deleteSubscription(subscriptionId: string): Observable<any> {
        const url = `${this.BASE_URL}/profile/delete-subscription/${subscriptionId}`;
        return this.http.delete<any>(url);
    }
}