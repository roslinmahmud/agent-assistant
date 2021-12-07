import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../../features/authentication/services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {

  constructor(private router: Router,
    private injector: Injector) {
     }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = this.handleError(error);
        return throwError(errorMessage);
      })
    )
  }

  private handleError = (error: HttpErrorResponse): string | HttpErrorResponse => {
    if(error.status === 404){
      return this.handleNotFound(error);
    }
    else if(error.status == 400){
      return this.handleBadRequest(error);
    }
    else if(error.status == 401){
      return this.handleUnauthorized(error);
    }
    else{
      return error;
    }
  }

  private handleNotFound = (error: HttpErrorResponse): string => {
    this.router.navigate(['/404']);
    return error.message;
  }

  private handleBadRequest = (error: HttpErrorResponse): string => {
    if(this.router.url == '/authentication/register'){
      return error.error.errors;
    }
    else{
      return error.error ? error.error : error.message;
    }
  }

  private handleUnauthorized = (error: HttpErrorResponse): string => {
    const authService = this.injector.get(AuthenticationService);
    if(this.router.url.startsWith('/authentication/login')){
      let message = error.error.errorMessage;
      return message;
    }
    else{
      this.router.navigate(['/authentication/login'], {queryParams:{returnUrl: this.router.url} });
      authService.sendAuthStateChangeNotification(false);
      return error.message;
    }
  }
}
