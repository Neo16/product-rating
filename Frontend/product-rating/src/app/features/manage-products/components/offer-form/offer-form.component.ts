import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ManageProductsService } from '../../services/manage-products.service';
import { CreateEditOfferData } from 'src/app/models/products/CreateEditOfferData';

@Component({
  selector: 'app-offer-form',
  templateUrl: './offer-form.component.html',
  styleUrls: ['./offer-form.component.scss']
})
export class OfferFormComponent implements OnInit {

  @Input() productId: string;
  price: number;
  url: string;
  alreadyExists: boolean;

  constructor(public activeModal: NgbActiveModal,
    public productService: ManageProductsService) { }

  ngOnInit() {
    this.productService.getOffer(this.productId)
      .subscribe(res => {
        this.price = res.price;
        this.url = res.url;
      });
  }

  makeOffer() {
    //create offer service call
    this.productService.addOffer(this.productId, { url: this.url, price: this.price } as CreateEditOfferData)
      .subscribe(res => {
        this.activeModal.close();
      });
  }

  deleteOffer() {
    //delete offer service call 
    this.productService.deletetOffer(this.productId)
      .subscribe(res => {
        this.activeModal.close();
      });
  }
}
