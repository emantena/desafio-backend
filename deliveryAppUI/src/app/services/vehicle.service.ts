import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseResponseModel } from '../models/response/baseResponseModel';
import { VehicleUpdateModel } from '../models/vehicleUpdateModel';
import { VehicleCreateModel } from '../models/vehicleCreateModel';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private URL_API: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getBrand(): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/vehicle/brand`);
  }

  getModel(brandId: number): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(
      `${this.URL_API}/vehicle/brand/${brandId}/model`
    );
  }

  getVehicles(): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/vehicle`);
  }

  getVehicleByPlate(plate: String): Observable<BaseResponseModel> {
    return this.http.get<BaseResponseModel>(`${this.URL_API}/vehicle/${plate}`);
  }

  remove(vehicleId: number): Observable<BaseResponseModel> {
    return this.http.delete<BaseResponseModel>(
      `${this.URL_API}/vehicle/${vehicleId}`
    );
  }

  update(
    vehicleUpdateModel: VehicleUpdateModel
  ): Observable<BaseResponseModel> {
    return this.http.patch<BaseResponseModel>(
      `${this.URL_API}/vehicle`,
      vehicleUpdateModel
    );
  }

  create(
    vehicleCreateModel: VehicleCreateModel
  ): Observable<BaseResponseModel> {
    return this.http.post<BaseResponseModel>(
      `${this.URL_API}/vehicle`,
      vehicleCreateModel
    );
  }
}
