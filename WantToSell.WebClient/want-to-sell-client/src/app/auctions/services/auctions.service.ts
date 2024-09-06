import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {AuctionDetailModel} from '../models/auction-item.model';
import {AuctionPagedList} from "../models/auction-paged.list";

@Injectable({
  providedIn: 'root'
})
export class AuctionsService {

  constructor(private http: HttpClient) { }

  private apiUrl = environment.apiUrl;

  fetchList(): Observable<AuctionPagedList> {
    return this.http.get<AuctionPagedList>(this.apiUrl + '/items');
  }

  fetchAuction(id: string): Observable<AuctionDetailModel> {
    return this.http.get<AuctionDetailModel>(this.apiUrl + `/items/${id}`);
  }

}
