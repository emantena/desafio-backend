import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseResponseModel } from '../models/response/baseResponseModel';
import { CreateDeliveryManModel } from '../models/createDeliveryManModel';

@Injectable({
  providedIn: 'root',
})
export class DeliverymanService {
  private URL_API: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  create(deliveryman: CreateDeliveryManModel): Observable<BaseResponseModel> {
    return this.http.post<BaseResponseModel>(
      `${this.URL_API}/deliveryman`,
      deliveryman
    );
  }
}
