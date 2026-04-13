import {Component, inject, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Card } from '../../models/card';
import { CardComponent} from '../card/card.component';
import { GameService} from '../../services/game.service';

@Component({
  selector: 'game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss'],
  standalone: true,
  imports: [CardComponent]
})

export class GameBoardComponent implements OnInit {
  gameService: GameService = inject(GameService);
  router: Router = inject(Router);
  route: ActivatedRoute = inject(ActivatedRoute);


  ngOnInit() {
    this.startNewGame();
  }

  async startNewGame() {
    const game = await this.gameService.startGame(0)
  }

  async selectedCard(card: Card) {
    await this.gameService.selectCard(card);
    card.selected = !card.selected;
  }

  deleteGame(){
    this.gameService.deleteGame();
    this.returnHome();
  }

  async showHint() {
    await this.gameService.showHint();
  }

  returnHome() {
    this.router.navigate(['/home']);
  }
}
