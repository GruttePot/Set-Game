import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'game-over',
  templateUrl: './game-over.component.html',
  styleUrls: ['./game-over.component.scss'],
  standalone: true
})
export class GameOverComponent {
  gameService: GameService = inject(GameService);
  router: Router = inject(Router);

  returnHome() {
    this.router.navigate(['/home']);
  }
}
