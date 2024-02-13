import { ReadEstadoDto } from "../endereco/readEstadoDto";
import { ReadCategoriaTribunalDto } from "./readCategoriaTribunalDto";

export interface ReadTribunalDto {
  Id: number;
  Nome: string;
  Sigla: string;
  Numero: string;
  EndpointConsultaNumero: string;
  Categoria: ReadCategoriaTribunalDto;
  Estado: ReadEstadoDto;
}
