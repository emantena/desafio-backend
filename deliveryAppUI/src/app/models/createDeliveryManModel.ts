export class CreateDeliveryManModel {
  name: string;
  email: string;
  birthDate: string;
  cnh: string;
  cnpj: string;
  cnhType: number;
  password: string;

  constructor(
    name: string,
    birthDate: string,
    cnh: string,
    cnpj: string,
    cnhType: number,
    password: string,
    email: string
  ) {
    this.name = name;
    this.email = email;
    this.birthDate = birthDate;
    this.cnh = cnh;
    this.cnpj = cnpj;
    this.cnhType = cnhType;
    this.password = password;
  }
}
