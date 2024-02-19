export class VehicleCreateModel {
  plate: string;
  modelId: number;
  yearManufacture: number;

  constructor(plate: string, modelId: number, yearManufacture: number) {
    this.plate = plate;
    this.modelId = modelId;
    this.yearManufacture = yearManufacture;
  }
}
