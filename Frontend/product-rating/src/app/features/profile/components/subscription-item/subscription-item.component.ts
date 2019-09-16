import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SubscriptionData } from 'src/app/models/profile/SubscriptionData';

@Component({
  selector: 'app-subscription-item',
  templateUrl: './subscription-item.component.html',
  styleUrls: ['./subscription-item.component.scss']
})
export class SubscriptionItemComponent implements OnInit {

  @Input() subscription: SubscriptionData;  
  @Output() deleteEvent: EventEmitter<string> = new EventEmitter<string>();

  
  constructor() { }

  ngOnInit() {
  }

  delete() {
    this.deleteEvent.emit(this.subscription.id);
  }
}
