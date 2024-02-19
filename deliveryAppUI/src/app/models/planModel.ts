export class PlanModel {
  planId: number;
  planVersionId: number;
  name: string;
  description: string;
  price: number;
  active: boolean;

  constructor(
    planId: number,
    planVersionId: number,
    name: string,
    description: string,
    price: number,
    active: boolean
  ) {
    this.planId = planId;
    this.planVersionId = planVersionId;
    this.name = name;
    this.description = description;
    this.price = price;
    this.active = active;
  }
}
