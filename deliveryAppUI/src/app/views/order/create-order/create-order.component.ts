import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CreateOrderModel } from 'src/app/models/createOrderModel';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { ErrorResponse } from 'src/app/models/response/errorResponse';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-create-vehicle',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css'],
})
export class CreateOrderComponent implements OnInit {
  errorMessage: string = '';

  orderModel: CreateOrderModel = new CreateOrderModel(0);
  orderForm: FormGroup = new FormGroup(
    {
      racePrice: new FormControl(''),
    },
  );

  constructor(private _router: Router, private orderService: OrderService) {}

  ngOnInit() {}

  createOrder(): void {
    this.orderModel.racePrice = this.orderForm.get('racePrice')?.value;
    
    this.orderService.create(this.orderModel).subscribe(
      (res: BaseResponseModel) => {
        this._router.navigate(['order']);
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
