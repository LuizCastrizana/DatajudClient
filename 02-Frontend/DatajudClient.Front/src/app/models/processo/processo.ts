import { IProcesso } from "../../interfaces/processo/processo";
import { Estado } from "../../interfaces/endereco/estado";
import { Tribunal } from "../../interfaces/tribunal/tribunal";
import { AndamentoProcesso } from "../../interfaces/processo/andamentoProcesso";
import { StringTools } from "../../util/stringtools";

export class Processo implements IProcesso {
    id: number;
    numeroProcesso: string;
    nomeCaso: string;
    vara: string | null;
    comarca: string;
    observacao: string | null;
    ultimoAndamento: Date;
    ultimoAndamentoDescricao: string;
    ultimaAtualizacao: Date;
    estado: Estado;
    tribunal: Tribunal;
    Andamentos: AndamentoProcesso[] | null;

    constructor() {
      this.id = 0;
      this.numeroProcesso = '';
      this.nomeCaso = '';
      this.vara = '';
      this.comarca = '';
      this.observacao = '';
      this.ultimoAndamento = new Date();
      this.ultimoAndamentoDescricao = '';
      this.ultimaAtualizacao = new Date();
      this.estado = {} as Estado;
      this.tribunal = {} as Tribunal;
      this.Andamentos = [];
    }

    getNumeroProcessoFormatado(): string {
        return StringTools.apenasNumeros(this.numeroProcesso).replace(/(\d{7})(\d{2})(\d{4})(\d{1})(\d{2})(\d{4})/, "$1-$2.$3.$4.$5.$6");
    }
}
