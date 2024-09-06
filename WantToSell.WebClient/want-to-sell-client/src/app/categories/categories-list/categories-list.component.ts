import {Component, OnInit} from '@angular/core';
import {CategoriesService} from "../services/categories.service";
import {CategoriesItemComponent} from "./categories-item/categories-item.component";
import {CategoryListItemModel} from "../models/category-list-item.model";

@Component({
  selector: 'app-categories-list',
  standalone: true,
  imports: [
    CategoriesItemComponent
  ],
  templateUrl: './categories-list.component.html',
  styleUrl: './categories-list.component.scss'
})
export class CategoriesListComponent implements OnInit {
  protected categoriesList!: CategoryListItemModel[];

  constructor(private categoriesService: CategoriesService) {
  }

  ngOnInit(): void {
    this.categoriesService.fetchList().subscribe(data => {
      if(Array.isArray(data)) {
        this.categoriesList = [...data];
      }
        console.log(this.categoriesList);
    });
  }
}
