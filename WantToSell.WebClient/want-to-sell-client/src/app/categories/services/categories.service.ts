import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {CategoryPagedList} from "../models/category-paged.list";
import {CategoryListItemModel} from "../models/category-list-item.model";

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(private http: HttpClient) { }

  private apiUrl = environment.apiUrl;

  fetchList(): Observable<CategoryListItemModel> {
    return this.http.get<CategoryListItemModel>(this.apiUrl + '/categories');
  }
}
