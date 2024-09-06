export interface PagedList<T>
{
  ascending: number;
  isNextPage: boolean;
  isPreviousPage: boolean;
  pageIndex: number;
  pageSize: number;
  sortColumn: string;
  totalRecords: number;
  items: T[];
}
