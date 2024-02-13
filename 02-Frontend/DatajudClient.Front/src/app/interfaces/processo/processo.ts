import { Estado } from "../endereco/estado";
import { Tribunal } from "../tribunal/tribunal";

export interface Processo {
  id: number;
  numeroProcesso: string;
  nomeCaso: string;
  vara: string | null;
  comarca: string;
  observacao: string | null;
  ultimoAndamento: Date;
  ultimaAtualizacao: Date;
  estado: Estado;
  tribunal: Tribunal;
}
