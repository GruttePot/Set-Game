import {Card} from './card';

export enum GameStatus {Active = 'Active', Finished = 'Finished', Paused = 'Paused' }

export type Game = {
  id: number;
  hints: number;
  fails: number;
  createdAt: Date;
  finishedAt?: Date;
  deck: Deck;
  foundSets: number;
  status: GameStatus;
}

export type Deck = {
  id: number;
  cards: Card[];
}

export type Hints = Card[];

export type FoundSet = {
  id: number;
  isSet: boolean;
  game: Game;
}

