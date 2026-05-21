import { inject, Injectable, signal } from '@angular/core';
import {HttpClient} from '@angular/common/http';

import { Card } from '../models/card';
import {Game, GameStatus, Hints,} from '../models/game';
import { FoundSet} from '../models/found-set';

import { environment} from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class GameService {
  private http: HttpClient = inject(HttpClient);

  public id = signal<number>(0);
  public hints = signal<number>(0);
  public fails= signal<number>(0);
  public availableSets = signal<number>(0);
  public foundSets = signal<FoundSet[]>([]);

  public status = signal<GameStatus>(GameStatus.Active);
  public createdAt = signal<Date>(new Date());
  public finishedAt = signal<Date | undefined>(undefined);

  public tableCards = signal<Card[]>([]);
  public deck = signal<number>(81);
  public selectedCard: Card[] = [];

  public startGame(Id: number): void {
    if (Id > 0) {
      this.http.get<Game>(`${environment.apiUrl}/${Id}`).subscribe({
        next: (game) => this.updateGame(game),
        error: (error) => console.error('Failed to start game', error)
      });
    } else {
      this.http.post<Game>(`${environment.apiUrl}/new`, {}).subscribe({
        next: (game) => this.updateGame(game),
        error: (error) => console.error('Failed to start game', error)
      });
    }
  }

  // Deze functie wordt gebruikt om de game bij te werken, tijdens het spelen.
  private updateGame(game: Game): void {
    this.id.set(game.id);
    this.hints.set(game.hints);
    this.fails.set(game.fails);
    this.availableSets.set(game.availableSets);
    this.foundSets.set(game.foundSets);
    this.createdAt.set(game.createdAt);
    this.status.set(game.status);
    this.finishedAt.set(game.finishedAt);
    this.tableCards.set(game.tableCards);
    this.deck.set(game.deck.length);
  }

  public deleteGame(id: number): void {
    if(!this.id) {
      throw new Error('No game to delete');
    }
    this.http.delete<Game>(`${environment.apiUrl}/${id}`).subscribe({
      next: () => this.id.set(0),
      error: (error) => console.error('Failed to delete game', error)
    });
  }

  public showHint(): void {
    this.http.get<Hints>(`${environment.apiUrl}/${this.id()}/hint`).subscribe({
      next: (hint) => this.applyHint(hint),
      error: (error) => console.error('Failed to get hint', error)
    });
  }

  private applyHint(hint: Hints): void {
    if (!hint || hint.length === 0) return;
    this.hints.set(this.hints() - 1);
    this.tableCards.update(cards =>
      cards.map(card => ({ ...card, hinted: hint.some(h => h.id === card.id) }))
    )};

  public checkSet(cardIds: number[]): void {
    this.http.post<Game>(`${environment.apiUrl}/${this.id()}/check-set`, cardIds).subscribe({
      next: (game) => {
        this.updateGame(game);
        this.clearSelection();
      },
      error: (error) => {
        console.error('Failed to check set', error);
        this.clearSelection();
      }
    });
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
      this.checkSet(cards);

      // Clear selectie na validatie, ongeacht of het een Set is
      this.selectedCard = [];
    }
  }
  private clearSelection(): void {
    this.selectedCard = [];
    this.tableCards.update(cards =>
      cards.map(card => ({ ...card, selected: false }))
    )};

}
