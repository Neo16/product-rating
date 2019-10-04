import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductCellComponent } from './components/product-cell/product-cell.component';
import { ProductService } from './services/product.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { ProductRoutingModule } from './products-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReviewsComponent } from './components/reviews/reviews.component';
import { ReviewItemComponent } from './components/review-item/review-item.component';
import { ReviewService } from './services/review.service';
import { BarRatingModule } from "ngx-bar-rating";
import { ScoringComponent } from './components/scoring/scoring.component';
import { OfferListComponent } from './components/offer-list/offer-list.component';
import { OfferItemComponent } from './components/offer-item/offer-item.component';

@NgModule({
  declarations: [
    ProductListComponent,
    ProductCellComponent,
    ProductDetailsComponent,
    ReviewsComponent,
    ReviewItemComponent,
    ScoringComponent,
    OfferListComponent,
    OfferItemComponent    
  ],
  imports: [    
    CommonModule,
    ProductRoutingModule,
    SharedModule,
    BarRatingModule
  ],
  exports: [ProductListComponent],
  providers: [
    ProductService,
    ReviewService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ]
})
export class ProductsModule { }
