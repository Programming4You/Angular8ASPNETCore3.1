import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';


@Injectable()
export class AuthInterceptor implements HttpInterceptor{

   constructor(private router:Router){}

      intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      const idToken = localStorage.getItem('token');
      if (idToken) {
        const cloned = request.clone({
          headers: request.headers.set("Authorization", 
                   "Bearer " + idToken)     
        });
        return next.handle(cloned);
      } 
      else {
        return next.handle(request);
      }

    }

}