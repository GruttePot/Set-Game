import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Card } from '../../models/card';
import { CardComponent} from '../card/card.component';
import { GameService} from '../../services/game.service';

@Component({
  selector: 'game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss'],
  standalone: true
})

export class GameBoardComponent {
  gameService: GameService = inject(GameService);


  constructor(private router: Router) {

  }

  async showHint() {
    await this.gameService.showHint();
  }

  returnHome() {
    this.router.navigate(['/home']);
  }
}
