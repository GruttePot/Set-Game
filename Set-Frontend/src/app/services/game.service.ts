import { inject, Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { firstValueFrom} from 'rxjs';

import { Card } from '../models/card';
import { Game, Hints, FoundSet } from '../models/game';

import { environment} from '../../environments/environment';


@Injectable({ providedIn: 'root' })
export class GameService {
  private http: HttpClient = inject(HttpClient);

  public id: number = 0;
  public hints: number = 0;
  public fails: number = 0;

  public createdAt: Date = new Date();
  public finishedAt?: Date;

  public hand: Card[] = [];
  public selectedCard: Card[] = [];

  public async startGame(Id: number): Promise<number> {
    const game = await firstValueFrom(
      Id > 0
    ? this.http.get<Game>(`${environment.apiUrl}/${Id}`)
    : this.http.post<Game>(`${environment.apiUrl}/new`, {})
  );

    if (!game) {
      throw new Error('Failed to start game');
    }

    this.updateGame(game);
    return game.id;
  }

  // Deze functie wordt gebruikt om de game bij te werken, tijdens het spelen.
  private updateGame(game: Game): void {
    this.id = game.id;
    this.hints = game.hints;
    this.fails = game.fails;
    this.createdAt = game.createdAt;
    this.finishedAt = game.finishedAt;

    this.hand = game.deck.cards;

  }

  public async deleteGame() {
    if(!this.id) {
      throw new Error('No game to delete');
    }
    await firstValueFrom(this.http.delete<Game>(`${environment.apiUrl}/${this.id}`));

    this.id = 0;
  }

  public async showHint(): Promise<Hints> {
    const hint = await firstValueFrom(this.http.get<Hints>(`${environment.apiUrl}/${this.id}/hint/`));

    if (hint) {
      this.hints--;
    }
    return hint;
  }

  public async checkSet(cardIds: number[]): Promise<boolean> {
  const foundSet = await firstValueFrom(this.http.post<FoundSet>(`${environment.apiUrl}/${this.id}/check-set`, cardIds));

  this.updateGame(foundSet.game);

  return foundSet.isSet;
  }

  public async getAvailableSets() {
    const sets = await firstValueFrom(this.http.get<number>(`${environment.apiUrl}/${this.id}/available-sets`));

    return sets;
  }

  public async selectCard(card: Card) {

    // Controller of een kaart in de hand zit
    if (!this.hand.includes(card)) {
      return;
    }

    // Deselecteer een kaart als deze al geselecteerd is (Toggle)
    const cardIndex = this.selectedCard.indexOf(card);
    if (cardIndex > -1) {
      this.selectedCard.splice(cardIndex, 1);
      return;
    }

    // Selecteer een kaart als 3 nog niet geslecteerd zijn
    if (this.selectedCard.length < 3) {
      this.selectedCard.push(card);
    }

    // Controlleer voor een Set als 3 kaarten geselecteerd zijn
    if (this.selectedCard.length === 3) {
      const cards = this.selectedCard.map(c => c.id);
      const isSet = await this.checkSet(cards);

      // Clear selectie na validatie, ongeacht of het een Set is
      this.selectedCard = [];
    }
  }
}
