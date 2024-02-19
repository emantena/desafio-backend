import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { AcceptOrderModel } from 'src/app/models/acceptOrderModel';
import { FinishOrderModel } from 'src/app/models/finishOrderModel';
import { OrderMessage } from 'src/app/models/orderMessage';
import { OrderModel } from 'src/app/models/ordersModel';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { OrderService } from 'src/app/services/order.service';
import { SignalRService } from 'src/app/services/signalR.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css'],
})
export class MyOrdersComponent implements OnInit {
  messages: string[] = [];
  result: OrderMessage[] = [];
  myOrders: OrderModel[] = [];

  constructor(
    private signalRService: SignalRService,
    private orderService: OrderService
  ) {}

  ngOnInit() {
    this.loadOrders();
    this.reciveMessages();
  }

  reciveMessages() {
    this.signalRService.message$.subscribe((message) => {
      const lowercaseJsonString = message.replace(
        /"(\w+)"\s*:/g,
        (match, p1) => `"${p1.toLowerCase()}" :`
      );
      const order = JSON.parse(lowercaseJsonString);

      this.result.push(order);
    });
  }

  accept(order: OrderMessage) {
    const acceptOrder = new AcceptOrderModel(order.orderid);

    this.orderService
      .accept(acceptOrder)
      .subscribe((res: BaseResponseModel) => {
        this.loadOrders();
        this.refuse(order);
      });
  }

  refuse(order: OrderMessage) {
    this.result = this.result.filter((x) => x.orderid !== order.orderid);
  }
  finish(order: OrderModel) {
    const finishOrder = new FinishOrderModel(order.orderId);

    this.orderService
      .finish(finishOrder)
      .subscribe((res: BaseResponseModel) => {
        this.loadOrders();
      });
  }

  loadOrders() {
    this.orderService.myOrders().subscribe((res: BaseResponseModel) => {
      this.myOrders = res.data as OrderModel[];
    });
  }
}
