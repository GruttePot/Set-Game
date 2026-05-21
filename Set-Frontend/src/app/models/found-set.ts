import {Card} from './card';
import {Game} from './game';

export type FoundSet = {
  id: number;
  card1: Card;
  card2: Card;
  card3: Card;
  game: Game;
  foundAt: Date;
}
