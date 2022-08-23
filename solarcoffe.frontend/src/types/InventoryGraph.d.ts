export interface IInventoryTimeline {
  timeline: Date[];
  productInventorySnaphots: ISnaphot[];
}

export interface ISnaphot {
  productId: number;
  quantityOnHand: number[];
}
