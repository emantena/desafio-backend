import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BaseResponseModel } from '../models/response/baseResponseModel';

@Injectable({
  providedIn: 'root',
})
export class UploadService {
  private API_URL = environment.apiUrl;

  constructor(private http: HttpClient) {}

  uploadFile(file: File) {
    const formData: FormData = new FormData();
    formData.append('file', file);

    //https://localhost:44365/api/DeliveryMan/1/upload/document/cnh

    const headers = new HttpHeaders({
      Accept: '*/*',
      'Content-Type':
        'multipart/form-data; boundary=<calculated when request is sent>',
    });

    return this.http.post<BaseResponseModel>(
      `${this.API_URL}/DeliveryMan/1/upload/document/cnh`,
      file,
      { headers }
    );
    // return this.http.post(this.API_URL + `upload`, formData)
    //     .pipe(map(this.httpUtil.extrairDados))
    //     .pipe(
    //         retryWhen(errors => errors.pipe(delay(1000), take(10))),
    //         catchError(this.httpUtil.processarErros));
  }
}
