import { ComplementoAndamento } from "./complementoAndamento";

export interface AndamentoProcesso {
  Id: number;
  Codigo: number;
  Descricao: string;
  DataHora: Date;
  Complementos: ComplementoAndamento[] | null;
}
