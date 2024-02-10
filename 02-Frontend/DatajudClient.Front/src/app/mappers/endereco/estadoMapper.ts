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
        id: Dto.id,
        nome: Dto.nome,
        uf: Dto.uf,
        codigoIbge: Dto.codigoIbge,
      }

      return estado;
    }

    public static ToDto(estado: Estado): ReadEstadoDto {
      let estadoDto: ReadEstadoDto = {
        id: estado.id,
        nome: estado.nome,
        uf: estado.uf,
        codigoIbge: estado.codigoIbge,
      }
      return estadoDto;
    }

  }
