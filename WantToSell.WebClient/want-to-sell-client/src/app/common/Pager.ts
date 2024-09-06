export class Pager
{
  public pageIndex: number;
  public pageSize: number;
  public sortColumn: string;
  public ascending: boolean;

  constructor(pageIndex: number, pageSize: number, sortColumn: string, ascending: boolean) {
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
    this.sortColumn = sortColumn;
    this.ascending = ascending;
  }
}
