import { Routes } from '@angular/router';
import { LoginComponent} from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard} from './services/auth.guard';


export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  //{ path: 'game', component: GameComponent, }
];
