import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LoggingService {

  logError(message: string) {
    // Send errors to be saved here
    console.log('LoggingService: ' + message);
  }
}