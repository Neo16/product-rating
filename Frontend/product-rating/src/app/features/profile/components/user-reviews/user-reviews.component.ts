import { Component, OnInit, Input } from '@angular/core';
import { TextReviewWithProductInfoData } from 'src/app/models/profile/TextReviewWithProductInfoData';
import { ProfileService } from '../../services/profile-service';

@Component({
  selector: 'app-user-reviews',
  templateUrl: './user-reviews.component.html',
  styleUrls: ['./user-reviews.component.scss']
})
export class UserReviewsComponent implements OnInit {

  @Input()
  userId: string;
  reviews: TextReviewWithProductInfoData[];

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.profileService.getReviewsMadeByUser(this.userId)
      .subscribe(res => {
        this.reviews = res;
      })
  }
}
