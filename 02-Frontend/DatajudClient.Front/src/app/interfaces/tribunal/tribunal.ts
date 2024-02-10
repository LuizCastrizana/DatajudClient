import { Estado } from "../endereco/estado";
import { CategoriaTribunal } from "./categoriaTribunal";

export interface Tribunal {
  id: number;
  nome: string;
  sigla: string;
  numero: string;
  endpointConsultaNumero: string;
  categoria: CategoriaTribunal;
  estado: Estado;
}
