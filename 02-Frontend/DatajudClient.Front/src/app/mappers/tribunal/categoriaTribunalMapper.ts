import { Injectable } from "@angular/core";
import { ReadCategoriaTribunalDto } from "../../dtos/tribunal/readCategoriaTribunalDto";
import { CategoriaTribunal } from "../../interfaces/tribunal/categoriaTribunal";

@Injectable({
  providedIn: "root",
})

export class CategoriaTribunalMapper {

    constructor() { }

    public static FromDto(Dto: ReadCategoriaTribunalDto): CategoriaTribunal {
      let categoriaTribunal: CategoriaTribunal = {
        id: Dto.Id,
        descricao: Dto.Descricao
      }

      return categoriaTribunal;
    }

    public static ToDto(categoriaTribunal: CategoriaTribunal): ReadCategoriaTribunalDto {
      let categoriaTribunalDto: ReadCategoriaTribunalDto = {
        Id: categoriaTribunal.id,
        Descricao: categoriaTribunal.descricao
      }
      return categoriaTribunalDto;
    }

  }
