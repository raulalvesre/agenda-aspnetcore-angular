import { JwtTokenService } from './../services/jwt-token.service';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class Interceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private snackBar: MatSnackBar,
    private jwtTokenService: JwtTokenService
    ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const jwtToken: string = this.jwtTokenService.jwtToken;
    if (!!jwtToken) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${jwtToken}`,
        }
      });
    }

    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status == 401 && !!jwtToken) {
            this.jwtTokenService.destroyToken();
            this.snackBar.open('Sessão expirada!', 'OK', { duration: 3000 });
            this.router.navigateByUrl('/login');
          }

          return throwError(error);
        })
      );
  }
}
