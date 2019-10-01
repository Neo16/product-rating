import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

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

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
    //TODO: get my offer (by productId) 
  }

  makeOffer() {
    //TODO: make offer service call
    this.activeModal.close();
  }

  deleteOffer() {
    //TODO: delete offer service call 
    this.activeModal.close();
  }
}
