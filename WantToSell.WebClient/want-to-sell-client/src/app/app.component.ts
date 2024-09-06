import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from "./header/header.component";
import {AuctionListComponent} from "./auctions/auction-list/auction-list.component";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {CategoriesListComponent} from "./categories/categories-list/categories-list.component";
import {ReactiveFormsModule} from "@angular/forms";
import {MatIconModule} from "@angular/material/icon";
import {MatError, MatSelectModule} from "@angular/material/select";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatNativeDateModule} from "@angular/material/core";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatSelectModule,
    MatIconModule,
    MatError,
    NgxMaterialTimepickerModule,
    RouterOutlet,
    FontAwesomeModule,
    ReactiveFormsModule,
    HeaderComponent,
    AuctionListComponent,
    CategoriesListComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'want-to-sell-client';
}
