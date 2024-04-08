import { AndamentoProcesso } from './../../interfaces/processo/andamentoProcesso';
import { Injectable } from "@angular/core";
import { ReadProcessoDto } from "../../dtos/processo/readProcessoDto";
import { Processo } from "../../models/processo/processo";
import { EstadoMapper } from "../endereco/estadoMapper";
import { TribunalMapper } from '../tribunal/tribunalMapper';
import { CreateProcessoDto } from '../../dtos/processo/createProcessoDto';
import { UpdateDadosProcessoDto } from '../../dtos/processo/updateDadosProcessoDto';
import { DeleteProcessoDto } from '../../dtos/processo/deleteProcessoDto';

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
      ultimoAndamentoDescricao: Dto.UltimoAndamentoDescricao,
      ultimaAtualizacao: Dto.UltimaAtualizacao,
      estado: EstadoMapper.FromDto(Dto.Estado),
      tribunal: TribunalMapper.FromDto(Dto.Tribunal),
      Andamentos: Dto.Andamentos
    } as Processo;

    return Object.assign(new Processo, processo);;
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
      UltimoAndamentoDescricao: processo.ultimoAndamentoDescricao,
      UltimaAtualizacao: processo.ultimaAtualizacao,
      Estado: EstadoMapper.ToDto(processo.estado),
      Tribunal: TribunalMapper.ToDto(processo.tribunal),
      Andamentos: processo.Andamentos!
    }
    return processoDto;
  }

  public static ToUpdateDto(processo: Processo): UpdateDadosProcessoDto {
    let processoDto: UpdateDadosProcessoDto = {
      NumeroProcesso: processo.numeroProcesso,
      NomeCaso: processo.nomeCaso,
      Vara: processo.vara,
      Comarca: processo.comarca,
      Observacao: processo.observacao,
      EstadoId: processo.estado.id,
      TribunalId: processo.tribunal.id
    }
    return processoDto;
  }

  public static ToUpdateDadosDto(processo: Processo): UpdateDadosProcessoDto {
    let processoDto: UpdateDadosProcessoDto = {
      NumeroProcesso: processo.numeroProcesso,
      NomeCaso: processo.nomeCaso,
      Vara: processo.vara,
      Comarca: processo.comarca,
      Observacao: processo.observacao,
      EstadoId: processo.estado.id,
      TribunalId: processo.tribunal.id
    }
    return processoDto;
  }

  public static ToCreateDto(processo: Processo): CreateProcessoDto {
    let processoDto: CreateProcessoDto = {
      NumeroProcesso: processo.numeroProcesso,
      NomeCaso: processo.nomeCaso,
      Vara: processo.vara,
      Comarca: processo.comarca,
      Observacao: processo.observacao,
      EstadoId: processo.estado.id,
      TribunalId: processo.tribunal.id
    }
    return processoDto;
  }

  public static ToDeleteDto(processo: Processo): DeleteProcessoDto {
    let processoDto: DeleteProcessoDto = {
      Ids: [processo.id]
    }
    return processoDto;
  }

}
