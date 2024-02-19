import { ErrorResponse } from './errorResponse';

export class BaseResponseModel {
  public success!: boolean;
  public status!: number;
  public data!: any;
  public error!: ErrorResponse;
}
