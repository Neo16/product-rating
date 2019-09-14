import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {    

    var token = localStorage.getItem('productrating-token');
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`,
          'content-type': 'application/json'
         }
      });    
   
    return next.handle(request);
  }
}

