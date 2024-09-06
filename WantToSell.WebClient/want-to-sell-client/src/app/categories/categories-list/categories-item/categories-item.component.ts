import {Component, Input} from '@angular/core';
import {MatCardHeader, MatCardTitle} from "@angular/material/card";
import {CategoryListItemModel} from "../../models/category-list-item.model";

@Component({
  selector: 'app-categories-item',
  standalone: true,
  imports: [
    MatCardHeader,
    MatCardTitle
  ],
  templateUrl: './categories-item.component.html',
  styleUrl: './categories-item.component.scss'
})
export class CategoriesItemComponent {
  @Input() category!: CategoryListItemModel;
}
