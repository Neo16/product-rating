import { Component, OnInit, Input } from '@angular/core';
import { OfferHeaderData } from 'src/app/models/products/OfferHeaderData';

@Component({
  selector: 'app-offer-item',
  templateUrl: './offer-item.component.html',
  styleUrls: ['./offer-item.component.scss']
})
export class OfferItemComponent {

  @Input()
  offer: OfferHeaderData;

  constructor() { }
}
