export class Vehicle {
  vehicleId: number;
  vehicleModelId: number;
  plate: string;
  yearManufacture: number;
  createAt: string;
  model: VehicleModel;

  constructor(
    vehicleId: number,
    vehicleModelId: number,
    plate: string,
    yearManufacture: number,
    createAt: string,
    model: VehicleModel
  ) {
    this.vehicleId = vehicleId;
    this.vehicleModelId = vehicleModelId;
    this.plate = plate;
    this.yearManufacture = yearManufacture;
    this.createAt = createAt;
    this.model = model;
  }
}

export class VehicleModel {
  modelId: number;
  model: string;
  brand: Brand;
  name: string;

  constructor(modelId: number, model: string, brand: Brand, name: string) {
    this.modelId = modelId;
    this.model = model;
    this.name = name;
    this.brand = brand;
  }
}

export class Brand {
  brandId: number;
  brand: string;
  name: string;

  constructor(brandId: number, brand: string, name: string) {
    this.brandId = brandId;
    this.name = brand;
    this.brand = brand;
  }
}
