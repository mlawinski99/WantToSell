import {Component, Input} from '@angular/core';
import {
  MatCard,
  MatCardContent,
  MatCardFooter,
  MatCardHeader,
  MatCardImage,
  MatCardMdImage,
  MatCardTitle
} from "@angular/material/card";
import { Router } from '@angular/router';
import {AuctionListItemModel} from "../../models/auction-list-item.model";

@Component({
  selector: 'app-auction-item',
  standalone: true,
  imports: [
    MatCardContent,
    MatCard,
    MatCardImage,
    MatCardHeader,
    MatCardFooter,
    MatCardMdImage
  ],
  templateUrl: './auction-item.component.html',
  styleUrl: './auction-item.component.scss'
})
export class AuctionItemComponent {
  @Input() auction!: AuctionListItemModel;

  constructor(private router: Router) {
  }

  viewAuctionDetails(auctionId: string): void {
    this.router.navigate(['/auctions', auctionId]);
  }
}
