import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { OrderRoutingModule } from './order.routing.module';
import { CreateOrderComponent } from './create-order/create-order.component';
import { ListOrderComponent } from './list-order/list-order.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';

@NgModule({
  declarations: [CreateOrderComponent, ListOrderComponent, MyOrdersComponent],

  imports: [CommonModule, ReactiveFormsModule, OrderRoutingModule, FormsModule],
})
export class OrderModule {}
