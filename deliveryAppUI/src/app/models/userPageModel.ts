import { UserModel } from './userModel';

export class UserPageModel {
  public totalPages!: number;
  public pageSize!: number;
  public currentPage!: number;
  public users!: UserModel[];
}
