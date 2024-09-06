import { Component, OnInit } from '@angular/core';
import { AuctionsService } from '../services/auctions.service';
import {AuctionDetailModel} from '../models/auction-item.model';
import {ActivatedRoute} from "@angular/router";
import {MatCard, MatCardContent, MatCardHeader, MatCardImage, MatCardTitle} from "@angular/material/card";
import {MatGridList, MatGridTile} from "@angular/material/grid-list";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-auction-detail',
  standalone: true,
  imports: [
    MatCardTitle,
    MatCardHeader,
    MatCard,
    MatCardContent,
    MatGridTile,
    MatGridList,
    DatePipe,
    MatCardImage
  ],
  templateUrl: './auction-detail.component.html',
  styleUrl: './auction-detail.component.scss'
})
export class AuctionDetailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private auctionsService: AuctionsService) {
  }

  protected auction!: AuctionDetailModel;
  private auctionId: string = '';

  ngOnInit()
  {
    this.route.paramMap.subscribe(params => {
      this.auctionId = params.get('id') as string;
        if (this.auctionId) {
          this.auctionsService.fetchAuction(this.auctionId).subscribe(data => {
            this.auction = data;
          });
      }
    });
  }

}
