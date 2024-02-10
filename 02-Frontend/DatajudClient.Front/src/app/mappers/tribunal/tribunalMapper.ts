import { Estado } from './../../interfaces/endereco/estado';
import { Injectable } from "@angular/core";
import { ReadTribunalDto } from "../../dtos/tribunal/readTribunalDto";
import { Tribunal } from "../../interfaces/tribunal/tribunal";
import { CategoriaTribunalMapper } from "./categoriaTribunalMapper";
import { EstadoMapper } from '../endereco/estadoMapper';

@Injectable({
  providedIn: "root",
})

export class TribunalMapper {

  constructor() { }

  public static FromDto(Dto: ReadTribunalDto): Tribunal {
    let tribunal: Tribunal = {
      id: Dto.id,
      nome: Dto.nome,
      sigla: Dto.sigla,
      numero: Dto.numero,
      endpointConsultaNumero: Dto.endpointConsultaNumero,
      categoria: CategoriaTribunalMapper.FromDto(Dto.categoria),
      estado: EstadoMapper.FromDto(Dto.estado),
    }

    return tribunal;
  }

  public static ToDto(tribunal: Tribunal): ReadTribunalDto {
    let tribunalDto: ReadTribunalDto = {
      id: tribunal.id,
      nome: tribunal.nome,
      sigla: tribunal.sigla,
      numero: tribunal.numero,
      endpointConsultaNumero: tribunal.endpointConsultaNumero,
      categoria: CategoriaTribunalMapper.ToDto(tribunal.categoria),
      estado: EstadoMapper.ToDto(tribunal.estado),
    }
    return tribunalDto;
  }

}
