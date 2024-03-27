import { Estado } from "../endereco/estado";
import { Tribunal } from "../tribunal/tribunal";
import { AndamentoProcesso } from "./andamentoProcesso";

export interface Processo {
  id: number;
  numeroProcesso: string;
  nomeCaso: string;
  vara: string | null;
  comarca: string;
  observacao: string | null;
  ultimoAndamento: Date;
  ultimoAndamentoDescricao: string;
  ultimaAtualizacao: Date;
  estado: Estado;
  tribunal: Tribunal;
  Andamentos: AndamentoProcesso[] | null;
}
