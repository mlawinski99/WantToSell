import {PagedList} from "../../common/PagedList";
import {CategoryListItemModel} from "./category-list-item.model";

export class CategoryPagedList implements PagedList<CategoryListItemModel> {
  ascending!: number;
  isNextPage!: boolean;
  isPreviousPage!: boolean;
  pageIndex!: number;
  pageSize!: number;
  sortColumn!: string;
  totalRecords!: number;
  items!: CategoryListItemModel[];
}
