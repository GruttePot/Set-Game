import {inject, Injectable} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable, tap } from "rxjs";

import { Credentials, LoginResponse} from '../models/credentials';
import { environment} from '../../environments/environment';



@Injectable({ providedIn: "root" })
export class AuthService {
  private http: HttpClient = inject(HttpClient);
  private router: Router = inject(Router);

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());

  login(credentials: Credentials): Observable<LoginResponse>
  {
    return this.http.post<LoginResponse>(`${environment.authUrl}`, credentials).pipe(
      tap(loginResponse => {
        if (loginResponse.token) {
        localStorage.setItem(environment.authTokenKey, loginResponse.token);
        this.isAuthenticatedSubject.next(true);
        }
      })
    );
  }

  logout(): void {
  localStorage.removeItem(environment.authTokenKey);
  this.isAuthenticatedSubject.next(false);
  this.router.navigate(['/login']);
  }

  getToken(): string | null
  {
  return localStorage.getItem(environment.authTokenKey);
  }

  private hasToken(): boolean
  {
  return !!localStorage.getItem(environment.authTokenKey);
  }

  isAuthenticated(): Observable<boolean>
  {
  return this.isAuthenticatedSubject.asObservable();
  }
}
