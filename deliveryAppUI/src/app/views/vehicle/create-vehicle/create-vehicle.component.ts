import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { ErrorResponse } from 'src/app/models/response/errorResponse';
import { Brand, VehicleModel } from 'src/app/models/vehicle';
import { VehicleCreateModel } from 'src/app/models/vehicleCreateModel';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-create-vehicle',
  templateUrl: './create-vehicle.component.html',
  styleUrls: ['./create-vehicle.component.css'],
})
export class CreateVehicleComponent implements OnInit {
  errorMessage: string = '';
  models: VehicleModel[] = [];
  brands: Brand[] = [];
  vehicle: VehicleCreateModel = new VehicleCreateModel('', 0, 0);

  constructor(
    private vehicleService: VehicleService,
    private _router: Router
  ) {}

  ngOnInit() {
    this.getBrands();
  }

  getBrands() {
    this.vehicleService.getBrand().subscribe((res: BaseResponseModel) => {
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

    this.vehicleService
      .getModel(parseInt(brandId))
      .subscribe((res: BaseResponseModel) => {
        this.models = res.data as VehicleModel[];
      });
  }

  createVehicle(): void {
    this.vehicleService.create(this.vehicle).subscribe(
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
