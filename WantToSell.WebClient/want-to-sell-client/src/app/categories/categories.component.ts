import { Component } from '@angular/core';
import {AuctionListComponent} from "../auctions/auction-list/auction-list.component";
import {CategoriesListComponent} from "./categories-list/categories-list.component";

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    AuctionListComponent,
    CategoriesListComponent
  ],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent {

}
