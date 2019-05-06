import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumToArrayPipe } from './pipes/enumToArray';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faTimes, faArrowDown, faArrowUp, faStar, faTrash, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { Ng5SliderModule } from 'ng5-slider';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@NgModule({
  declarations: [EnumToArrayPipe],
  imports: [
    Ng5SliderModule,
    FontAwesomeModule,
    FormsModule,
    NgbModule.forRoot(),
    CommonModule,
    NgxDatatableModule
  ],
  exports: [
    NgbModule,
    Ng5SliderModule,
    FontAwesomeModule,
    FormsModule,
    EnumToArrayPipe,
    NgxDatatableModule
  ]
})
export class SharedModule {
  constructor() {
    // Add an icon to the library for convenient access in other components
    library.add(faTimes);
    library.add(faArrowDown);
    library.add(faArrowUp);
    library.add(faStar); 
    library.add(faTrash); 
    library.add(faTrashAlt); 
  }
 }
