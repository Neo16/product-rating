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
   score: number = 0;

  ngOnInit() {
    console.log(this.productid);
    this.service.getProductScore(this.productid)
      .subscribe(result => {
          this.score = result;
      })
  }   
}