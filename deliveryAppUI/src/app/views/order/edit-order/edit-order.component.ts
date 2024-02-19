import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { ErrorResponse } from 'src/app/models/response/errorResponse';
import { Brand, Vehicle, VehicleModel } from 'src/app/models/vehicle';
import { VehicleUpdateModel } from 'src/app/models/vehicleUpdateModel';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.css'],
})
export class EditOrderComponent implements OnInit {
  errorMessage: string = '';
  models: VehicleModel[] = [];
  brands: Brand[] = [];
  vehicle: Vehicle = new Vehicle(
    0,
    0,
    '',
    0,
    '',
    new VehicleModel(0, '', new Brand(0, '', ''), '')
  );

  constructor(
    private _vehicleService: VehicleService,
    private _router: Router,
    private _activateRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getBrands();

    this._activateRoute.params.subscribe((params) => {
      const plate = params['plate'];
      this._vehicleService
        .getVehicleByPlate(plate)
        .subscribe((res: BaseResponseModel) => {
          this.vehicle = res.data as Vehicle;
        });
    });
  }

  getBrands() {
    this._vehicleService.getBrand().subscribe((res: BaseResponseModel) => {
      this.brands = res.data as Brand[];
    });
  }

  public getModels(event: Event): void {
    let brandId = (<HTMLSelectElement>event.target).value;

    brandId = brandId.replace(/[^\d]+/g, '');

    if (brandId === '' || brandId === null) {
      this.models = [];
      return;
    }

    this._vehicleService
      .getModel(parseInt(brandId))
      .subscribe((res: BaseResponseModel) => {
        this.models = res.data as VehicleModel[];
      });
  }

  updateVehicle(): void {
    const vehicleModelUpdate = new VehicleUpdateModel(
      this.vehicle.vehicleId,
      this.vehicle.plate
    );

    this._vehicleService.update(vehicleModelUpdate).subscribe(
      (res: BaseResponseModel) => {
        this._router.navigate(['vehicle']);
      },
      (err: BaseResponseModel) => {
        console.log(err);
        if (err.error) {
          const errorMessage = JSON.stringify(err.error);
          const loginErrorModel = JSON.parse(errorMessage) as ErrorResponse;
          this.errorMessage = loginErrorModel.errors[0].message;
        }
      }
    );
  }
}
