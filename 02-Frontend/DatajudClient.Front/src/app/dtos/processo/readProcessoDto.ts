import { ReadEstadoDto } from "../endereco/readEstadoDto";
import { ReadCategoriaTribunalDto } from "../tribunal/readCategoriaTribunalDto";
import { ReadTribunalDto } from "../tribunal/readTribunalDto";

export interface ReadProcessoDto {
  id: number;
  numeroProcesso: string;
  nomeCaso: string;
  vara: string | null;
  comarca: string;
  observacao: string | null;
  estado: ReadEstadoDto;
  tribunal: ReadTribunalDto;
}
