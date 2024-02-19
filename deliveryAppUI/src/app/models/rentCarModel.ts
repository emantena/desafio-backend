export class RentCarModel {
  deliveryManId: number;
  planVersionId: number;
  leaseEndDate: Date;

  constructor(
    deliveryManId: number,
    planVersionId: number,
    leaseEndDate: Date
  ) {
    this.deliveryManId = deliveryManId;
    this.planVersionId = planVersionId;
    this.leaseEndDate = leaseEndDate;
  }
}
