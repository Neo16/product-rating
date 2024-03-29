import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumToArrayPipe } from './pipes/enumToArray';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faTimes, faArrowDown, faArrowUp, faStar, faTrash, faTrashAlt, faEdit, faPencilAlt, faSave } from '@fortawesome/free-solid-svg-icons';
import { Ng5SliderModule } from 'ng5-slider';
import { NgbModule, NgbDatepicker} from '@ng-bootstrap/ng-bootstrap';
import { UiSwitchModule } from 'ngx-ui-switch';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PopupComponent } from './popup/popup.component';
import { ModalService } from './services/modal-service';
import { LongPress } from './directives/long-press';

@NgModule({
  declarations: [EnumToArrayPipe, PopupComponent, LongPress],
  providers: [ModalService],
  imports: [
    Ng5SliderModule,
    FontAwesomeModule,
    FormsModule,
    NgbModule.forRoot(),
    CommonModule,
    NgxDatatableModule,
    UiSwitchModule,
    NgbModule
  ],
  exports: [
    NgbModule,
    Ng5SliderModule,
    FontAwesomeModule,
    FormsModule,
    EnumToArrayPipe,
    NgxDatatableModule,
    UiSwitchModule,
    NgbModule,
    PopupComponent,
    LongPress
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
    library.add(faEdit);
    library.add(faPencilAlt);
    library.add(faSave);  
  }
 }
