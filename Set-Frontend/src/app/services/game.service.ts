import { inject, Injectable, signal } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { firstValueFrom} from 'rxjs';

import { Card } from '../models/card';
import { Game, Hints, FoundSet } from '../models/game';

import { environment} from '../../environments/environment';


@Injectable({ providedIn: 'root' })
export class GameService {
  private http: HttpClient = inject(HttpClient);

  public id = signal<number>(0);
  public hints = signal<number>(0);
  public fails= signal<number>(0);
  public foundSets = signal<number>(0);

  public createdAt = signal<Date>(new Date());
  public finishedAt = signal<Date | undefined>(undefined);

  public tableCards = signal<Card[]>([]);
  public deck = signal<number>(81);
  public selectedCard: Card[] = [];

  public async startGame(Id: number): Promise<number> {
    let game: Game;
     if (Id > 0)
     {
       game = await firstValueFrom(this.http.get<Game>(`${environment.apiUrl}/${Id}`))
     } else {
       game = await firstValueFrom(this.http.post<Game>(`${environment.apiUrl}/new`, {}));
     }

    if (!game) {
      throw new Error('Failed to start game');
    }

    this.updateGame(game);
    return game.id;
  }

  // Deze functie wordt gebruikt om de game bij te werken, tijdens het spelen.
  // /update endpoint toepassen voor persistence?
  private updateGame(game: Game): void {
    this.id.set(game.id);
    this.hints.set(game.hints);
    this.fails.set(game.fails);
    this.foundSets.set(game.foundSets);
    this.createdAt.set(game.createdAt);
    this.finishedAt.set(game.finishedAt);
    this.tableCards.set(game.tableCards);
    this.deck.set(game.deck.length);
  }

  public async deleteGame() {
    if(!this.id) {
      throw new Error('No game to delete');
    }
    await firstValueFrom(this.http.delete<Game>(`${environment.apiUrl}/${this.id()}`));

    this.id.set(0);
  }

  public async showHint(): Promise<Hints> {
    const hint = await firstValueFrom(this.http.get<Hints>(`${environment.apiUrl}/${this.id()}/hint`));

    if (hint && hint.length > 0) {
      this.hints.set(this.hints() - 1);

      this.tableCards.update(cards => {
        const updated = cards.map(card => {
          const isHinted = hint.some(h => h.id === card.id);
          console.log(`Card ${card.id}: hinted=${isHinted}`);
          return {
            ...card,
            hinted: isHinted
          };
        });
        console.log('Updated tableCards with hinted property:', updated);
        return updated;
      });
    } else {
      console.log('No hint received or hint is empty');
    }
    return hint;
  }

  public async checkSet(cardIds: number[]): Promise<boolean> {
  const foundSet = await firstValueFrom(this.http.post<FoundSet>(`${environment.apiUrl}/${this.id()}/check-set`, cardIds));

  this.updateGame(foundSet.game);

  return foundSet.isSet;
  }

  public async getAvailableSets() {
    const sets = await firstValueFrom(this.http.get<number>(`${environment.apiUrl}/${this.id()}/available-sets`));

    return sets;
  }

  public async selectCard(card: Card) {

    // Controller of een kaart in de hand zit
    if (!this.tableCards().includes(card)) {
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
