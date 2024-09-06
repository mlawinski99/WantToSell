export class AuctionDetailModel
{
  public id!: string;
  public name!: string;
  public description!: string;
  public dateExpiredUtc!: Date;
  public condition!: string;
  public category!: string;
  public subcategory!: string;
  public images!: IFormFile[];
}

interface IFormFile {
  name: string;
  type: string;
  size: number;
}
