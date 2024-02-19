export class UpdateUserModel {
  public userId: string;
  public celphone: string;
  public creci: string;

  constructor(userId: string, celphone: string, creci: string,) {
    this.userId = userId;
    this.celphone = celphone;
    this.creci = creci;
  }
}
