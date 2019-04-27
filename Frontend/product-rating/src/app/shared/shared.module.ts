import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumToArrayPipe } from './pipes/enumToArray';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { Ng5SliderModule } from 'ng5-slider';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [EnumToArrayPipe],
  imports: [
    Ng5SliderModule,
    FontAwesomeModule,
    FormsModule,
    NgbModule.forRoot(),
    CommonModule
  ],
  exports: [
    NgbModule,
    Ng5SliderModule,
    FontAwesomeModule,
    FormsModule,
    EnumToArrayPipe
  ]
})
export class SharedModule {
  constructor() {
    // Add an icon to the library for convenient access in other components
    library.add(faTimes);
  }
 }
