import { Injectable } from "@angular/core";
import { ReadEstadoDto } from "../../dtos/endereco/readEstadoDto";
import { Estado } from "../../interfaces/endereco/estado";

@Injectable({
  providedIn: "root",
})

export class EstadoMapper {

    constructor() { }

    public static FromDto(Dto: ReadEstadoDto): Estado {
      let estado: Estado = {
        id: Dto.Id,
        nome: Dto.Nome,
        uf: Dto.UF,
        codigoIbge: Dto.CodigoIbge,
      }

      return estado;
    }

    public static ToDto(estado: Estado): ReadEstadoDto {
      let estadoDto: ReadEstadoDto = {
        Id: estado.id,
        Nome: estado.nome,
        UF: estado.uf,
        CodigoIbge: estado.codigoIbge,
      }
      return estadoDto;
    }

  }
