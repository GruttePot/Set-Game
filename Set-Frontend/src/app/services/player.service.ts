import { inject, Injectable } from "@angular/core";
import { HttpClient} from '@angular/common/http';

import { AuthService } from "./auth.service";
import { Credentials } from '../models/credentials';
import { Game } from '../models/game';

import { environment} from '../../environments/environment';
import { firstValueFrom } from 'rxjs';


@Injectable({ providedIn: "root" })
export class PlayerService {
  private http: HttpClient = inject(HttpClient);
  private authService: AuthService = inject(AuthService);

  // public async getPlayerInfo() {
  //   const token = this.authService.getToken();
  //   if (!token) {
  //     throw new Error("No authentication token found");
  //   }
  // }

  public async playerLogin(credentials: Credentials): Promise<void> {
    await firstValueFrom(this.authService.login(credentials));
  }

  public async getGames(): Promise<Game[]> {
    const games = await firstValueFrom(this.http.get<Game[]>(`${environment.apiUrl}`))

    return games.sort((a: Game, b: Game)=>
      new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()).reverse();
  }
}
