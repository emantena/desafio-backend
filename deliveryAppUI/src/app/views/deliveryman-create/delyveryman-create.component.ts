import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CreateDeliveryManModel } from 'src/app/models/createDeliveryManModel';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { ErrorResponse } from 'src/app/models/response/errorResponse';
import { DeliverymanService } from 'src/app/services/deliveryman.service';

@Component({
  selector: 'app-delyveryman-create',
  templateUrl: './delyveryman-create.component.html',
  styleUrls: ['./delyveryman-create.component.css'],
})
export class DelyverymanCreateComponent implements OnInit {
  errorMessage: string = '';
  deliveryman: CreateDeliveryManModel = new CreateDeliveryManModel(
    '',
    '',
    '',
    '',
    0,
    '',
    ''
  );

  deliverymanForm: FormGroup = new FormGroup({});

  constructor(
    private _router: Router,
    private fb: FormBuilder,
    private deliverymanService: DeliverymanService
  ) {}

  ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.deliverymanForm = this.fb.group({
      name: [this.deliveryman.name, Validators.required],
      birthDate: [this.deliveryman.birthDate, Validators.required],
      cnh: [this.deliveryman.cnh, Validators.required],
      cnpj: [this.deliveryman.cnpj, Validators.required],
      cnhType: [this.deliveryman.cnhType, Validators.required],
      email: [this.deliveryman.email, Validators.required],
      password: [this.deliveryman.password, Validators.required],
    });
  }

  createDeliveryman() {
    this.deliveryman.name = this.deliverymanForm.get('name')?.value;
    this.deliveryman.birthDate = this.deliverymanForm.get('birthDate')?.value;
    this.deliveryman.cnh = this.deliverymanForm.get('cnh')?.value;
    this.deliveryman.cnpj = this.deliverymanForm.get('cnpj')?.value;
    this.deliveryman.cnhType = this.deliverymanForm.get('cnhType')?.value;
    this.deliveryman.email = this.deliverymanForm.get('email')?.value;
    this.deliveryman.password = this.deliverymanForm.get('password')?.value;

    this.deliverymanService.create(this.deliveryman).subscribe(
      (res: BaseResponseModel) => {
        this._router.navigate(['login']);
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
