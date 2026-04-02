import {inject, Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import { AuthService} from './auth.service';
import { Observable, map, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);

  canActivate() : Observable<boolean> {
    return this.authService.isAuthenticated().pipe(
      take(1),
      map(isAuthenticated => {
        if (isAuthenticated) {
          return true;
        } else {
          this.router.navigate(['/login']);
          return false;
        }
      })
    );
  }
}
