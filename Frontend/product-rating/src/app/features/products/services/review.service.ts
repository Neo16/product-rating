import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReviewData } from 'src/app/models/reviews/ReviewData';
import { CreateReviewData } from 'src/app/models/reviews/CreateReviewData';
import { CreateScoreData } from 'src/app/models/reviews/CreateScoreData';
import { ReviewMood } from 'src/app/models/reviews/ReviewMood';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class ReviewService {
  private BASE_URL = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

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

  addScore(score: CreateScoreData) {
    const url = `${this.BASE_URL}/reviews/add-scrore`;
    return this.http.post(url, score);
  }

  editReview(reviewId: string, text: string, mood: ReviewMood): Observable<any> {
    const url = `${this.BASE_URL}/reviews/${reviewId}`;

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