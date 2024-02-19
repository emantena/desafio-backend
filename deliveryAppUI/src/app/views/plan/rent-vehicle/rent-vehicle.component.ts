import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Route, Router } from '@angular/router';
import { PlanModel } from 'src/app/models/planModel';
import { RentCarModel } from 'src/app/models/rentCarModel';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { ErrorResponse } from 'src/app/models/response/errorResponse';
import { PlanService } from 'src/app/services/plan.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-rent-vehicle',
  templateUrl: './rent-vehicle.component.html',
  styleUrls: ['./rent-vehicle.component.css'],
})
export class RentVehicleComponent implements OnInit {
  plans: PlanModel[] = [];
  plan: number = 0;
  errorMessage: string = '';

  rentVehicleForm: FormGroup = new FormGroup({});

  result: any = null;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private planService: PlanService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.rentVehicleForm = this.fb.group({
      startDate: ['startDate', Validators.required],
      endDate: ['endDate', Validators.required],
      planId: ['planId', Validators.required],
    });

    this.getPlan();
  }

  selectedPlan(event: Event) {
    const selectedPlanId = (<HTMLSelectElement>event.target).value;
    this.plan = parseInt(selectedPlanId);
    this.rentVehicleForm.get('plan')?.setValue(this.plan);
  }

  getPlan() {
    this.planService.getPlansActives().subscribe((res: BaseResponseModel) => {
      this.plans = res.data as PlanModel[];
    });
  }

  rentVehicle() {
    const endDate = this.rentVehicleForm.get('endDate')?.value;

    const rentCar = new RentCarModel(
      this.userService.getUserId(),
      this.plan,
      endDate
    );

    this.planService.rentVehicle(rentCar).subscribe(
      (res: BaseResponseModel) => {
        this.router.navigate(['/dashboard']);
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

  checkPrice() {
    const startDate = this.rentVehicleForm.get('startDate')?.value;
    const endDate = this.rentVehicleForm.get('endDate')?.value;
    this.planService.calculatePrice(this.plan, startDate, endDate).subscribe(
      (res: BaseResponseModel) => {
        this.result = res;
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
