export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  isTaxable: boolean;
  createdOn: Date;
  updatedOn: Date;
  isArchived: boolean;
}

export interface IProductInventory {
  id: number;
  product: IProduct;
  quantityOnHand: number;
  idealQuantity: number;
}
