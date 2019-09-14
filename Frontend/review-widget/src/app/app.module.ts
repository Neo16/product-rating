import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faArrowDown, faArrowUp, faTrash, faTrashAlt, faPencilAlt, faSave, fas } from '@fortawesome/free-solid-svg-icons';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { EnumToArrayPipe } from './pipes/enumToArray';
import { CommonModule } from '@angular/common';
import { ReviewsComponent } from './components/reviews/reviews.component';
import { ReviewItemComponent } from './components/review-item/review-item.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { TokenInterceptor } from './token-interceptor';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    FontAwesomeModule,
    CommonModule,
    NgbModule,
    HttpClientModule
  ],
  declarations: [
    ReviewsComponent,
    ReviewItemComponent,
    EnumToArrayPipe,
    LoginFormComponent
  ],
  entryComponents: [ReviewsComponent],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }]
})
export class AppModule {
  constructor(private injector: Injector, library: FaIconLibrary) {
    library.addIconPacks(fas);
    library.addIcons(faArrowDown);
    library.addIcons(faArrowUp);
    library.addIcons(faTrash);
    library.addIcons(faTrashAlt);
    library.addIcons(faPencilAlt);
    library.addIcons(faSave);
  }

  ngDoBootstrap() {
    // using createCustomElement from angular package it will convert angular component to stander web component
    const el = createCustomElement(ReviewsComponent, {
      injector: this.injector
    });
    // using built in the browser to create your own custome element name
    customElements.define('reviews-section', el);
  }
}


