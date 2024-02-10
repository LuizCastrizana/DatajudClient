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
        id: Dto.id,
        descricao: Dto.descricao
      }

      return categoriaTribunal;
    }

    public static ToDto(categoriaTribunal: CategoriaTribunal): ReadCategoriaTribunalDto {
      let categoriaTribunalDto: ReadCategoriaTribunalDto = {
        id: categoriaTribunal.id,
        descricao: categoriaTribunal.descricao
      }
      return categoriaTribunalDto;
    }

  }
