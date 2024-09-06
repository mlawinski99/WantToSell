import { Component } from '@angular/core';
import {AuctionCreateFormComponent} from "./auction-create-form/auction-create-form.component";

@Component({
  selector: 'app-auction-create',
  standalone: true,
  imports: [
    AuctionCreateFormComponent
  ],
  templateUrl: './auction-create.component.html',
  styleUrl: './auction-create.component.scss'
})
export class AuctionCreateComponent {
}
