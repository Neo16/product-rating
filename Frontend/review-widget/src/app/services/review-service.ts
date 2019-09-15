import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReviewData } from '../models/ReviewData';
import { CreateReviewData } from '../models/CreateReviewData';
import { ReviewMood } from '../models/ReviewMood';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private BASE_URL = 'https://localhost:44394';
  private API_KEY = '';

  constructor(private http: HttpClient) { }

  setApiKey(key: string) {
    this.API_KEY = key;
  }

  getReviewsOfProduct(productId: string): Observable<ReviewData[]> {
    const url = `${this.BASE_URL}/products/${productId}/reviews?key=${this.API_KEY}`;
    return this.http.get<ReviewData[]>(url);
  }

  upvoteReview(reviewId: string): Observable<any> {
    const url = `${this.BASE_URL}/reviews/upvote-review?key=${this.API_KEY}`;
    return this.http.post<any>(url, { id: reviewId });
  }

  downvoteReview(reviewId: string): Observable<any> {
    const url = `${this.BASE_URL}/reviews/downvote-review?key=${this.API_KEY}`;
    return this.http.post<any>(url, { id: reviewId });
  }

  addNewReview(review: CreateReviewData): Observable<ReviewData> {
    const url = `${this.BASE_URL}/reviews/add-review?key=${this.API_KEY}`;
    return this.http.post<ReviewData>(url, review);
  }
  editReview(reviewId: string, text: string, mood: ReviewMood): Observable<any> {
    const url = `${this.BASE_URL}/reviews/${reviewId}/update?key=${this.API_KEY}`;

    var editedReview: CreateReviewData = {
      mood: mood,
      text: text,
      productId: null
    };

    return this.http.put<any>(url, JSON.stringify(editedReview));
  }

  deleteReview(reviewId: string): Observable<any> {
    const url = `${this.BASE_URL}/reviews/${reviewId}?key=${this.API_KEY}`;
    return this.http.delete<any>(url);
  }

}