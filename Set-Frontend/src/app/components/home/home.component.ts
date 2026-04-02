import {Component, inject, OnInit} from '@angular/core';
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
export class HomeComponent implements OnInit {
  playerService: PlayerService = inject(PlayerService);
  router: Router = inject(Router);
  games: Game[] | undefined;

  async ngOnInit() {
    await this.getGames();
  }

  routeTo(target: string): void {
    this.router.navigate([target]);
  }

  async getGames() {
    this.games = await this.playerService.getGames();
  }

  gotoGame(game: number) {
    this.router.navigate(['/game-board', game]);
  }

}
