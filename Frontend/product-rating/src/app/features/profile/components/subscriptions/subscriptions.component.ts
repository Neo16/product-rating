import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../services/profile-service';
import { SubscriptionData } from 'src/app/models/profile/SubscriptionData';
import { RequireSubscriptionData } from 'src/app/models/profile/RequireSubscriptionData';

@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.scss']
})
export class SubscriptionsComponent implements OnInit {

  subscriptions: SubscriptionData[];
  newSubscription: RequireSubscriptionData = new RequireSubscriptionData();
  showForm: boolean = false;

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.loadSubscriptions();
  }

  loadSubscriptions() {
    this.profileService.getSubscriptions()
      .subscribe(result => {
        this.subscriptions = result;
      })
  }

  addSubscription() {
    this.profileService.requireSubscription(this.newSubscription)
      .subscribe(e => {
        this.loadSubscriptions();
        this.showForm = false;
      })
  }

  delete(id: string) {
    this.profileService.deleteSubscription(id)
      .subscribe(e => {
        this.loadSubscriptions();
      });
  }

  findSubscription(id: string) {
    return this.subscriptions
      .find(x => x.id === id);
  }

}
