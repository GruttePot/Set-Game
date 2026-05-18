import {Component, inject, signal} from '@angular/core';
import {Router} from '@angular/router';
import {DatePipe } from '@angular/common';
import {PlayerService } from '../../services/player.service';
import {GameService} from '../../services/game.service';
import {Game } from '../../models/game';

@Component({
  selector: 'home',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.scss'],
  imports: [DatePipe],
  standalone: true
})
export class HomeComponent {
  playerService: PlayerService = inject(PlayerService);
  gameService: GameService = inject(GameService);
  router: Router = inject(Router);

  games = this.playerService.games;

  constructor() { this.getGames(); }

  startNewGame(id: number) {
    this.gameService.startGame(id);
    setTimeout(() => {
      this.router.navigate(['/game-board', this.gameService.id()]);
    }, 100);
  }

  getGames() {
    this.playerService.getGames();
  }

  deleteGame(gameId: number) {
    if (confirm('Do you want to delete this game?')) {
      this.gameService.deleteGame(gameId);
      setTimeout(() => this.getGames(), 100);
    }
  }

  gotoGame(game: number) {
    this.router.navigate(['/game-board', game]);
  }
}
