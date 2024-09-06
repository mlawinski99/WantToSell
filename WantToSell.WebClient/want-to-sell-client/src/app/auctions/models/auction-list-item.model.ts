export class AuctionListItemModel
{
  public id: string;
  public name: string;
  public price: number;
  public photo: string;

  constructor(id: string, name: string, price: number, photo: string) {
    this.id = id;
    this.name = name;
    this.price = price;
    this.photo = photo;
  }
}

