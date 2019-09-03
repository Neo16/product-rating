import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';

// Note: this project is an example of 
// extractint an angular component to use it outside angular projects 
// using a new package called angular elements.

@Component({
  selector: 'app-score-element',
  templateUrl: './score-element.component.html',
  styleUrls: ['./score-element.component.scss'],
 // encapsulation: ViewEncapsulation.ShadowDom
})
export class ScoreElementComponent implements OnInit {

  constructor() { }

  @Input() init: number;
  number: number = 0;

  ngOnInit() {
    this.number = this.init;
  }  

  add(){
    this.number++;
  }
}
