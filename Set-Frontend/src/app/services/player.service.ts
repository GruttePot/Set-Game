import {inject, Injectable, signal} from "@angular/core";
import { HttpClient} from '@angular/common/http';

import { AuthService } from "./auth.service";
import { Game } from '../models/game';

import { environment} from '../../environments/environment';

@Injectable({ providedIn: "root" })
export class PlayerService {
  private http: HttpClient = inject(HttpClient);
  private authService: AuthService = inject(AuthService);
  public games = signal<Game[]>([]);

  public getGames(): void {
    this.http.get<Game[]>(`${environment.apiUrl}`).subscribe({
      next: (games) => {
        const sortedGames = games.sort((a: Game, b: Game) =>
          new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
        );
        this.games.set(sortedGames);
      },
      error: (error) => console.error('Failed to get games', error)
    });
  }
}
