import { Tribunal } from './../../interfaces/tribunal/tribunal';
import { Injectable } from "@angular/core";
import { ReadProcessoDto } from "../../dtos/processo/readProcessoDto";
import { Processo } from "../../interfaces/processo/processo";
import { EstadoMapper } from "../endereco/estadoMapper";
import { TribunalMapper } from '../tribunal/tribunalMapper';

@Injectable({
  providedIn: "root",
})

export class ProcessoMapper {

  constructor() { }

  public static FromDto(Dto: ReadProcessoDto): Processo {
    let processo: Processo = {
      id: Dto.id,
      numeroProcesso: Dto.numeroProcesso,
      nomeCaso: Dto.nomeCaso,
      vara: Dto.vara,
      comarca: Dto.comarca,
      observacao: Dto.observacao,
      estado: EstadoMapper.FromDto(Dto.estado),
      tribunal: TribunalMapper.FromDto(Dto.tribunal)
    }

    return processo;
  }

  public static ToDto(processo: Processo): ReadProcessoDto {
    let processoDto: ReadProcessoDto = {
      id: processo.id,
      numeroProcesso: processo.numeroProcesso,
      nomeCaso: processo.nomeCaso,
      vara: processo.vara,
      comarca: processo.comarca,
      observacao: processo.observacao,
      estado: EstadoMapper.ToDto(processo.estado),
      tribunal: TribunalMapper.ToDto(processo.tribunal)
    }
    return processoDto;
  }

}
