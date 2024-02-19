export class CharacteristicModel {
  caracteristicaId: number;
  tipoCaracteristicaId: number;
  descricao: string;

  constructor(
    caracteristicaId: number,
    tipoCaracteristicaId: number,
    descricao: string
  ) {
    this.caracteristicaId = caracteristicaId;
    this.descricao = descricao;
    this.tipoCaracteristicaId = tipoCaracteristicaId;
  }
}
