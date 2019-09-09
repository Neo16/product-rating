import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { LoggingService } from './core/services/logger.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  
   constructor(private injector: Injector) { }

  handleError(error) {    
    const logger = this.injector.get(LoggingService);  

    if (error instanceof HttpErrorResponse) {
        // Server Error
        if (error.status == 400 && error.error.errorMessages != null){         
            //TODO: popup 
            var message = error.error.errorMessages.join(' ,');
            alert(message);
            logger.logError(message);  
         } 
         else{
            console.log(error);            
         }      
    }    
    console.log(error);            
  }
}