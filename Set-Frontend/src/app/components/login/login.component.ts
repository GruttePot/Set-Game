import {Component, inject} from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService} from '../../services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [FormsModule],
})
export class LoginComponent {
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);

  username: string = '';
  password: string = '';
  errorMessage: string = '';

  onSubmit() {
    if (!this.username || !this.password) {
      this.errorMessage = 'Username en password zijn verplicht';
      return;
    }

    this.errorMessage = '';

    this.authService.login({ userName: this.username, PasswordHash: this.password}).subscribe({
      next: () => {
        this.router.navigate(['/home']);
      },
      error: (error) => {
        this.errorMessage = 'Ongeldige gegevens'
        console.error('Login error: ', error);
      }
    });
  }
}
