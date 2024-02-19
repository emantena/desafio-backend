export class ChangePasswordModel {
  public userId: string;
  public oldPassword: string;
  public newPassword: string;
  public confirmPassword: string;

  constructor(userId: string, oldPassword: string,
    newPassword: string,
    confirmNewPassword: string) {
    this.userId = userId;
    this.oldPassword = oldPassword;
    this.newPassword = newPassword;
    this.confirmPassword = confirmNewPassword;
  }
}
