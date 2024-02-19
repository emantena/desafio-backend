export class OrderMessage {
  orderid: number;
  userid: number;
  raceprice: number;

  constructor(orderId: number, userId: number, racePrice: number) {
    this.orderid = orderId;
    this.raceprice = racePrice;
    this.userid = userId;
  }
}
