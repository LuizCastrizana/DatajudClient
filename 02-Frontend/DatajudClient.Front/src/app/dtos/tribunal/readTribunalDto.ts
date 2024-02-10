import { ReadEstadoDto } from "../endereco/readEstadoDto";
import { ReadCategoriaTribunalDto } from "./readCategoriaTribunalDto";

export interface ReadTribunalDto {
  id: number;
  nome: string;
  sigla: string;
  numero: string;
  endpointConsultaNumero: string;
  categoria: ReadCategoriaTribunalDto;
  estado: ReadEstadoDto;
}
