import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { ErrorResponse } from 'src/app/models/response/errorResponse';
import { Vehicle } from 'src/app/models/vehicle';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-list-vehicle',
  templateUrl: './list-vehicle.component.html',
  styleUrls: ['./list-vehicle.component.css'],
})
export class ListVehicleComponent implements OnInit {
  public errorMessage = '';

  vehicles: Vehicle[] = [];
  searchPlate: string = '';
  filteredVehicles: Vehicle[] = [];

  constructor(
    private vehicleService: VehicleService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.getVehicles();
  }

  getVehicles(): void {
    this.vehicleService.getVehicles().subscribe((res: BaseResponseModel) => {
      this.vehicles = res.data as Vehicle[];
    });
  }

  editVehicle(vehicle: Vehicle): void {
    this._router.navigate(['vehicle/edit/' + vehicle.plate]);
  }

  deleteVehicle(vehicle: Vehicle): void {
    let result: any;

    this.vehicleService.remove(vehicle.vehicleId).subscribe(
      (res: BaseResponseModel) => {
        this.vehicles = this.vehicles.filter(
          (x) => x.vehicleId !== vehicle.vehicleId
        );
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

  searchVehicles(): void {
    if (this.searchPlate === '') {
      this.getVehicles();
      return;
    }

    this.vehicleService
      .getVehicleByPlate(this.searchPlate)
      .subscribe((res: BaseResponseModel) => {
        this.vehicles = [];
        this.vehicles.push(res.data as Vehicle);
      });
  }
}
