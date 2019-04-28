import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReviewData } from 'src/app/models/reviews/ReviewData';

@Injectable()
export class ReviewService {
  private BASE_URL = 'https://localhost:44394';

  constructor(private http: HttpClient) {}

  getReviewsOfProduct(productId: string){    
    const url = `${this.BASE_URL}/products/${productId}/reviews`;
    return this.http.get<ReviewData[]>(url);
  }

  upvoteReview(reviewId: string){    
    const url = `${this.BASE_URL}/reviews/upvote-review`;
    return this.http.post<any>(url, {id: reviewId});
  }

  downvoteReview(reviewId: string){    
    const url = `${this.BASE_URL}/reviews/downvote-review`;
    return this.http.post<any>(url, {id: reviewId});
  }


}