import { Component, OnInit } from '@angular/core';
import { ProductDetailsData } from 'src/app/models/products/ProductDetailsData';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { ReviewService } from '../../services/review.service';
import { AccountState } from 'src/app/store/account-store/account.state';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { selectAccountState } from 'src/app/store/root-state';
import { CreateScoreData } from 'src/app/models/reviews/CreateScoreData';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  isLoggedIn: boolean = false;
  product: ProductDetailsData = null;

  //stores
  getAccountState: Observable<AccountState>;

  constructor(
    private router: Router,
    private productSerive: ProductService,
    private reviewSercice: ReviewService,
    private acccountStore: Store<AccountState>
  ) {
    this.getAccountState = this.acccountStore.select(selectAccountState);
  }

  ngOnInit() {
    // the productId is after the last '/' in the url á
    var productId = this.router.url.split(/[\/ ]+/).pop();

    this.productSerive.getProductDetails(productId)
      .subscribe(result => {
        this.product = result;
      });

    this.getAccountState.subscribe((accountState) => {
      this.isLoggedIn = accountState.isAuthenticated;
    });
  }

  addScore(score: number) {
    var scoreData = {
        score: score,
        productId: this.product.id
    } as CreateScoreData;

    this.reviewSercice.addScore(scoreData)
     .subscribe(e => {
        this.product.scoreByMe = score;
     });
  }
}
