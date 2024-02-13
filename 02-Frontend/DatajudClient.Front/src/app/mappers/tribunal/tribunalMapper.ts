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
      id: Dto.Id,
      nome: Dto.Nome,
      sigla: Dto.Sigla,
      numero: Dto.Numero,
      endpointConsultaNumero: Dto.EndpointConsultaNumero,
      categoria: CategoriaTribunalMapper.FromDto(Dto.Categoria),
      estado: EstadoMapper.FromDto(Dto.Estado),
    }

    return tribunal;
  }

  public static ToDto(tribunal: Tribunal): ReadTribunalDto {
    let tribunalDto: ReadTribunalDto = {
      Id: tribunal.id,
      Nome: tribunal.nome,
      Sigla: tribunal.sigla,
      Numero: tribunal.numero,
      EndpointConsultaNumero: tribunal.endpointConsultaNumero,
      Categoria: CategoriaTribunalMapper.ToDto(tribunal.categoria),
      Estado: EstadoMapper.ToDto(tribunal.estado),
    }
    return tribunalDto;
  }

}
