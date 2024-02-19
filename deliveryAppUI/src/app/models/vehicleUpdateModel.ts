export class VehicleUpdateModel {
  vehicleId: number;
  plate: string;

  constructor(vehicleId: number, plate: string) {
    this.vehicleId = vehicleId;
    this.plate = plate;
  }
}
