import { Routes } from '@angular/router';
import { LoginComponent} from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard} from './services/auth.guard';
import {GameBoardComponent} from './components/game-board/game-board.component';
import {CardComponent} from './components/card/card.component';
import {GameOverComponent} from './components/game-over/game-over.component';


export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'card', component: CardComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'game-board', component: GameBoardComponent, canActivate: [AuthGuard] },
  { path: 'game-board/:id', component: GameBoardComponent, canActivate: [AuthGuard] },
  { path: 'game-over', component: GameOverComponent, canActivate: [AuthGuard] },
];
