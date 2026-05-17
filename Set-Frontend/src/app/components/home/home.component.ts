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
  games = signal<Game[]>([]);

  constructor() { this.getGames(); }

  async startNewGame(id: number) {
    const game = await this.gameService.startGame(id);
    this.router.navigate(['/game-board', game]);
  }

  async getGames() {
    this.games.set(await this.playerService.getGames());
  }

  deleteGame(){
    if (confirm('Do you want to delete this game?')) {
      this.gameService.deleteGame();
      this.getGames();
    }
  }

  gotoGame(game: number) {
    this.router.navigate(['/game-board', game]);
  }

}
