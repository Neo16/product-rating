import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { ScoreService } from '../score-service';

// Note: this project is an example of 
// extractint an angular component to use it outside angular projects 
// using a new package called angular elements.

@Component({
  selector: 'app-score-element',
  templateUrl: './score-element.component.html',
  styleUrls: ['./score-element.component.scss']
})
export class ScoreElementComponent implements OnInit {

  constructor(private service: ScoreService) { }

   @Input() productid: string;
   @Input() key: string;
   score: number = 0;

  ngOnInit() { 
    this.service.getProductScore(this.productid, this.key)
      .subscribe(result => {
          this.score = result;
      })
  }   
}
