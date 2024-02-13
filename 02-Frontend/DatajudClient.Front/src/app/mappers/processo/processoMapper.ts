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
      id: Dto.Id,
      numeroProcesso: Dto.NumeroProcesso,
      nomeCaso: Dto.NomeCaso,
      vara: Dto.Vara,
      comarca: Dto.Comarca,
      observacao: Dto.Observacao,
      ultimoAndamento: Dto.UltimoAndamento,
      ultimaAtualizacao: Dto.UltimaAtualizacao,
      estado: EstadoMapper.FromDto(Dto.Estado),
      tribunal: TribunalMapper.FromDto(Dto.Tribunal)
    }

    return processo;
  }

  public static ToDto(processo: Processo): ReadProcessoDto {
    let processoDto: ReadProcessoDto = {
      Id: processo.id,
      NumeroProcesso: processo.numeroProcesso,
      NomeCaso: processo.nomeCaso,
      Vara: processo.vara,
      Comarca: processo.comarca,
      Observacao: processo.observacao,
      UltimoAndamento: processo.ultimoAndamento,
      UltimaAtualizacao: processo.ultimaAtualizacao,
      Estado: EstadoMapper.ToDto(processo.estado),
      Tribunal: TribunalMapper.ToDto(processo.tribunal)
    }
    return processoDto;
  }

}
