export class OrderModel {
  orderId: number;
  deliveryManName: string;
  racePrice: number;
  status: string;

  constructor(
    orderId: number,
    deliveryManName: string,
    racePrice: number,
    status: string
  ) {
    this.orderId = orderId;
    this.deliveryManName = deliveryManName;
    this.racePrice = racePrice;
    this.status = status;
  }
}
