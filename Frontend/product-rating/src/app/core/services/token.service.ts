import { Injectable, Injector } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AccountService } from './account.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private authService: AccountService;
  constructor(private injector: Injector) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> { 
    this.authService = this.injector.get(AccountService);

    var needContentType =  request.url.indexOf("upload-picture") == -1;    

    const token: string = this.authService.getToken();   

    if (needContentType){
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`,
          'content-type': 'application/json'
         }
      });
    }
    else{
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`        
         }
      });
    }   
    return next.handle(request);
  }
}

