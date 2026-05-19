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
  availableSets: number;
  foundSets: number;
  status: GameStatus;
}

export type Hints = Card[];


