import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseResponseModel } from '../models/response/baseResponseModel';
import { RentCarModel } from '../models/rentCarModel';

@Injectable({
  providedIn: 'root',
})
export class PlanService {
  private URL_API: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getPlans(): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/plan`);
  }

  getPlansActives(): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/plan/active`);
  }

  calculatePrice(
    planId: number,
    startDate: string,
    endDate: string
  ): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(
      `${this.URL_API}/VehicleRent/price/${planId}?startDate=${startDate}&returnDate=${endDate}`
    );
  }

  rentVehicle(rentCar: RentCarModel) {
    return this.http.post<BaseResponseModel>(
      `${this.URL_API}/VehicleRent/`,
      rentCar
    );
  }
}
