import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReviewData } from '../models/ReviewData';
import { CreateReviewData } from '../models/CreateReviewData';
import { ReviewMood } from '../models/ReviewMood';

@Injectable({
    providedIn: 'root'
})
export class ReviewService{
    private BASE_URL = 'https://localhost:44394';

    constructor(private http:HttpClient){}
  
    getReviewsOfProduct(productId: string): Observable<ReviewData[]> {
      const url = `${this.BASE_URL}/products/${productId}/reviews`;
      return this.http.get<ReviewData[]>(url);
    }
  
    upvoteReview(reviewId: string): Observable<any> {
      const url = `${this.BASE_URL}/reviews/upvote-review`;
      return this.http.post<any>(url, { id: reviewId });
    }
  
    downvoteReview(reviewId: string): Observable<any> {
      const url = `${this.BASE_URL}/reviews/downvote-review`;
      return this.http.post<any>(url, { id: reviewId });
    }
  
    addNewReview(review: CreateReviewData): Observable<ReviewData> {
      const url = `${this.BASE_URL}/reviews/add-review`;
      return this.http.post<ReviewData>(url, review);
    }
    editReview(reviewId: string, text: string, mood: ReviewMood): Observable<any> {
      const url = `${this.BASE_URL}/reviews/${reviewId}/update`;
  
      var editedReview: CreateReviewData = {
        mood: mood,
        text: text,
        productId: null
      };
            
      return this.http.put<any>(url, JSON.stringify(editedReview));
    }
  
    deleteReview(reviewId: string): Observable<any> {
      const url = `${this.BASE_URL}/reviews/${reviewId}`;
      return this.http.delete<any>(url);
    }

}