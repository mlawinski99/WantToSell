import { Routes } from '@angular/router';
import { AuctionDetailComponent } from './auctions/auction-detail/auction-detail.component';
import { AuctionsComponent } from './auctions/auctions.component';
import {CategoriesComponent} from "./categories/categories.component";
import {AuctionCreateComponent} from "./auctions/auction-create/auction-create.component";
import {RegisterComponent} from "./auth/register/register.component";
import {LoginComponent} from "./auth/login/login.component";

export const routes: Routes = [
  { path: '', redirectTo: '/auctions', pathMatch: 'full' },
  { path: 'auctions', component: AuctionsComponent },
  { path: 'auctions/create', component: AuctionCreateComponent },
  { path: 'auctions/:id', component: AuctionDetailComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
];
