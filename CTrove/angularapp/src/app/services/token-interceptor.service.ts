import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { HttpHandler, HttpRequest } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService {

  constructor(private accountService: AccountService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token: string = this.accountService.getToken()
    if (token && req.url.includes(environment.apiUrl)) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }
    return next.handle(req).pipe()
  }
}
