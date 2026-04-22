import {Component, inject, signal} from '@angular/core';
import {Router} from '@angular/router';
import {DatePipe } from '@angular/common';
import {PlayerService } from '../../services/player.service';
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
  router: Router = inject(Router);
  games = signal<Game[]>([]);

  constructor() { this.getGames(); }

  routeTo(target: string): void {
    this.router.navigate([target]);
  }

  async getGames() {
    this.games.set(await this.playerService.getGames());
  }

  gotoGame(game: number) {
    this.router.navigate(['/game-board', game]);
  }

}
