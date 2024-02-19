import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseResponseModel } from '../models/response/baseResponseModel';
import { CreateOrderModel } from '../models/createOrderModel';
import { AcceptOrderModel } from '../models/acceptOrderModel';
import { OrderModel } from '../models/ordersModel';
import { FinishOrderModel } from '../models/finishOrderModel';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private URL_API: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  create(createOrderModel: CreateOrderModel): Observable<BaseResponseModel> {
    return this.http.post<BaseResponseModel>(
      `${this.URL_API}/order`,
      createOrderModel
    );
  }

  accept(acceptOrderModel: AcceptOrderModel): Observable<BaseResponseModel> {
    return this.http.post<BaseResponseModel>(
      `${this.URL_API}/order/accept`,
      acceptOrderModel
    );
  }

  finish(order: FinishOrderModel): Observable<BaseResponseModel> {
    return this.http.patch<BaseResponseModel>(
      `${this.URL_API}/order/finish`,
      order
    );
  }

  myOrders(): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/order/my-orders`);
  }

  getOrders(): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/order`);
  }
}
