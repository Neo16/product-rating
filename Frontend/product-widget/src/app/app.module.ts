import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import {createCustomElement} from '@angular/elements'
import { ScoreElementComponent } from './score-element/score-element.component';

@NgModule({
  imports: [
    BrowserModule
  ],
  declarations: [ 
    ScoreElementComponent
  ],
  entryComponents: [ScoreElementComponent]

  
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
