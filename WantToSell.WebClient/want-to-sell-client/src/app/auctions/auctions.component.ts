import { Component } from '@angular/core';
import {AuctionListComponent} from "./auction-list/auction-list.component";

@Component({
  selector: 'app-auctions',
  standalone: true,
  imports: [
    AuctionListComponent
  ],
  templateUrl: './auctions.component.html',
  styleUrl: './auctions.component.scss'
})
export class AuctionsComponent {

}
