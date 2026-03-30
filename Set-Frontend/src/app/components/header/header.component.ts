import { Component } from "@angular/core";
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {Observable} from 'rxjs';

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"],
  standalone: true
})

export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  constructor(private router: Router, private authService: AuthService) {
    this.isAuthenticated$ = this.authService.isAuthenticated();
  }

  routeTo(target: string)
  {
    this.router.navigate([target]);
  }

  logout(): void {
    this.authService.logout();
  }
}
