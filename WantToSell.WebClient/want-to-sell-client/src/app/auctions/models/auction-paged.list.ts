import {PagedList} from "../../common/PagedList";
import {AuctionListItemModel} from "./auction-list-item.model";

export class AuctionPagedList implements PagedList<AuctionListItemModel> {
  ascending!: number;
  isNextPage!: boolean;
  isPreviousPage!: boolean;
  pageIndex!: number;
  pageSize!: number;
  sortColumn!: string;
  totalRecords!: number;
  items!: AuctionListItemModel[];
}
