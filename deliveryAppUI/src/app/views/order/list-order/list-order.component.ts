import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { OrderModel } from 'src/app/models/ordersModel';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { Vehicle } from 'src/app/models/vehicle';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-list-order',
  templateUrl: './list-order.component.html',
  styleUrls: ['./list-order.component.css'],
})
export class ListOrderComponent implements OnInit {
  public errorMessage = '';

  orders: OrderModel[] = [];

  constructor(private orderService: OrderService, private _router: Router) {}

  ngOnInit(): void {
    this.getorders();
  }

  getorders(): void {
    this.orderService.getOrders().subscribe((res: BaseResponseModel) => {
      this.orders = res.data as OrderModel[];
    });
  }
}
