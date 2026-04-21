import {Card} from './card';

export enum GameStatus {Active = 'Active', Finished = 'Finished', Paused = 'Paused' }

export type Game = {
  id: number;
  hints: number;
  fails: number;
  createdAt: Date;
  finishedAt?: Date;
  deck: Card[];
  tableCards: Card[];
  foundSets: number;
  status: GameStatus;
}

export type Hints = Card[];

export type FoundSet = {
  id: number;
  isSet: boolean;
  game: Game;
}

