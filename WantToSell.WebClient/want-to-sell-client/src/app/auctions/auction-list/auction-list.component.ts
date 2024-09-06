import { Component, OnInit } from '@angular/core';
import {AuctionItemComponent} from "./auction-item/auction-item.component";
import { AuctionsService } from '../services/auctions.service';

import {AuctionPagedList} from "../models/auction-paged.list";

@Component({
  selector: 'app-auction-list',
  standalone: true,
  imports: [
    AuctionItemComponent
  ],
  templateUrl: './auction-list.component.html',
  styleUrl: './auction-list.component.scss'
})
export class AuctionListComponent implements OnInit {
  protected auctionList!: AuctionPagedList;

constructor(private auctionsService: AuctionsService) {
}

ngOnInit(): void {
  this.auctionsService.fetchList().subscribe(data => {
    this.auctionList = data
  });
}
}
