import { Component, OnInit, Input } from '@angular/core';
import { OfferHeaderData } from 'src/app/models/products/OfferHeaderData';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.scss']
})
export class OfferListComponent implements OnInit {

  @Input()
  productId: string;
  offers: OfferHeaderData[];

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.getOffers(this.productId)
      .subscribe(res => {
        this.offers = res;
      })
  }
}
