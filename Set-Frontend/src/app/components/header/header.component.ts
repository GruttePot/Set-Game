import {Component, inject} from "@angular/core";
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
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);

  isAuthenticated$: Observable<boolean> = this.authService.isAuthenticated()

  routeTo(target: string)
  {
    this.router.navigate([target]);
  }

  logout(): void {
    this.authService.logout();
  }
}
