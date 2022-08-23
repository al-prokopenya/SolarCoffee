export interface IInventoryTimeline {
  timeLine: Date[];
  productInventorySnapshots: ISnaphot[];
}

export interface ISnaphot {
  productId: number;
  quantityOnHand: number[];
}
