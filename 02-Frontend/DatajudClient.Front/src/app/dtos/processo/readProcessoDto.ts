import { AndamentoProcesso } from "../../interfaces/processo/andamentoProcesso";
import { ReadEstadoDto } from "../endereco/readEstadoDto";
import { ReadCategoriaTribunalDto } from "../tribunal/readCategoriaTribunalDto";
import { ReadTribunalDto } from "../tribunal/readTribunalDto";

export interface ReadProcessoDto {
  Id: number;
  NumeroProcesso: string;
  NomeCaso: string;
  Vara: string | null;
  Comarca: string;
  Observacao: string | null;
  UltimoAndamento: Date;
  UltimaAtualizacao: Date;
  Estado: ReadEstadoDto;
  Tribunal: ReadTribunalDto;
  Andamentos: AndamentoProcesso[];
}
