import {Component, inject, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Card } from '../../models/card';
import { GameStatus } from '../../models/game';
import { CardComponent} from '../card/card.component';
import { GameService} from '../../services/game.service';
import {FoundSetComponent} from '../found-set/found-set.component';

@Component({
  selector: 'game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss'],
  standalone: true,
  imports: [CardComponent,FoundSetComponent ]
})

export class GameBoardComponent implements OnInit {
  gameService: GameService = inject(GameService);
  router: Router = inject(Router);
  route: ActivatedRoute = inject(ActivatedRoute);

  ngOnInit() {
    const gameId = this.route.snapshot.paramMap.get('id');
    if (gameId) {
      this.gameService.startGame(parseInt(gameId));
    }
  }

  selectedCard(card: Card) {
    this.gameService.selectCard(card);
    card.selected = !card.selected;

    if (this.gameService.status() === GameStatus.Finished) {
      this.router.navigate(['/game-over']);
    }
  }

  showHint() {
    this.gameService.showHint();

    if (this.gameService.status() === GameStatus.Finished) {
      this.router.navigate(['/game-over']);
    }
  }

  returnHome() {
    this.router.navigate(['/home']);
  }
}
