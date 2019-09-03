import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import {createCustomElement} from '@angular/elements'
import { ScoreElementComponent } from './score-element/score-element.component';
import { BarRatingModule } from "ngx-bar-rating";
import { ScoreService } from './score-service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    BrowserModule,
    BarRatingModule,
    HttpClientModule   
  ],
  declarations: [ 
    ScoreElementComponent
  ],
  entryComponents: [ScoreElementComponent],
  // providers: [ScoreService]
  
})
export class AppModule { 
  constructor(private injector: Injector) {}

  ngDoBootstrap() {
    // using createCustomElement from angular package it will convert angular component to stander web component
    const el = createCustomElement(ScoreElementComponent, {
      injector: this.injector
    });
    // using built in the browser to create your own custome element name
    customElements.define('score-card', el);
  }

}
