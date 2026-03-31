import {Component} from '@angular/core';
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
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {
  }

  onSubmit() {
    if (!this.username || !this.password) {
      this.errorMessage = 'Username en password zijn verplicht';
      return;
    }

    this.errorMessage = '';

    this.authService.login({ userName: this.username, PasswordHash: this.password}).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (error) => {
        this.errorMessage = 'Ongeldige gegevens'
        console.error('Login error: ', error);
      }
    });
  }
}
