import { AndamentoProcesso } from './../../../interfaces/processo/andamentoProcesso';
import { Processo } from './../../../models/processo/processo';
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProcessoService } from '../../../services/processo/processo.service';
import { RespostaApiService } from '../../../services/shared/resposta-api.service';
import { DadosPaginador } from '../../../interfaces/shared/paginacao/dadosPaginador';
import { DadosPaginados } from '../../../interfaces/shared/paginacao/dadosPaginados';
import { ProcessoMapper } from '../../../mappers/processo/processoMapper';
import { ItemPagina } from '../../../interfaces/shared/paginacao/itemPagina';
import { Tribunal } from '../../../interfaces/tribunal/tribunal';

@Component({
  selector: 'app-detalhes-processo',
  templateUrl: './detalhes-processo.component.html',
  styleUrl: './detalhes-processo.component.css'
})
export class DetalhesProcessoComponent {

  Ordem: string = '';
  NomeCampo: string = '';

  Id: string | null = "";
  Processo: Processo = {
    tribunal: { } as Tribunal,
  } as Processo;

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 15,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<AndamentoProcesso> = { } as DadosPaginados<AndamentoProcesso>;

  constructor(
    private processoService: ProcessoService,
    private respostaApiService: RespostaApiService,
    private Router: Router,
    private ActivatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.Id = this.ActivatedRoute.snapshot.paramMap.get('id');
    this.processoService.buscarPorId(this.Id!).subscribe({
      next: (resposta) => {
        this.Processo = ProcessoMapper.FromDto(resposta.dados);
        this.ordenarAndamentos("Data");
      },
      error:(error) => {
        this.respostaApiService.tratarRespostaApi(error);
        this.Router.navigate(['/processos']);
      }
    });
  }

  ordenarAndamentos(nomeCampo?: string): void {
    if (nomeCampo == this.NomeCampo) {
      if (this.Ordem == "desc") {
        this.Ordem = "asc";
      } else if (this.Ordem == "asc") {
        this.Ordem = '';
        this.NomeCampo = '';
        nomeCampo = '';
      }
    } else {
      this.Ordem = '';
    }

    switch (nomeCampo) {
      case "Codigo":
        this.NomeCampo = "Codigo";
        if (this.Ordem == "asc") {
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => a.Codigo - b.Codigo);
          }
        } else {
          this.Ordem = "desc";
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => b.Codigo - a.Codigo);
          }
        }
        break;
      case "Descricao":
        this.NomeCampo = "Descricao";
        if (this.Ordem == "asc") {
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => a.Descricao.localeCompare(b.Descricao));
          }
        } else {
          this.Ordem = "desc";
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => b.Descricao.localeCompare(a.Descricao));
          }
        }
        break;
      case "Data":
        this.NomeCampo = "Data";
        if (this.Ordem == "asc") {
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => new Date(a.DataHora).getTime() - new Date(b.DataHora).getTime());
          }
        } else {
          this.Ordem = "desc";
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => new Date(b.DataHora).getTime() - new Date(a.DataHora).getTime());
          }
        }
        break;
      case "Complementos":
        this.NomeCampo = "Complementos";
        if (this.Ordem == "asc") {
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => a.ComplementosDescricao!.localeCompare(b.ComplementosDescricao!));
          }
        } else {
          this.Ordem = "desc";
          if (this.Processo.Andamentos != null) {
            this.Processo.Andamentos.sort((a, b) => b.ComplementosDescricao!.localeCompare(a.ComplementosDescricao!));
          }
        }
        break;
      default:
        this.NomeCampo = '';
        if (this.Processo.Andamentos != null) {
          this.Processo.Andamentos.sort((a, b) => new Date(b.DataHora).getTime() - new Date(a.DataHora).getTime());
        }
        break;
    }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.DadosPaginador.TotalItens = this.Processo.Andamentos!.length;
    this.paginarAndamentos();
  }

  tratarIconeOrdenacao(): void {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgCodigo = document.getElementById('imgCodigo');
    let imgDescricao = document.getElementById('imgDescricao');
    let imgData = document.getElementById('imgData');
    let imgComplementos = document.getElementById('imgComplementos');

    imgCodigo!.innerHTML = '';
    imgDescricao!.innerHTML = '';
    imgData!.innerHTML = '';
    imgComplementos!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'Codigo':
        if (this.Ordem == 'asc') {
          imgCodigo!.innerHTML = imgAsc;
        } else {
          imgCodigo!.innerHTML = imgDesc;
        }
        break;
      case 'Descricao':
        if (this.Ordem == 'asc') {
          imgDescricao!.innerHTML = imgAsc;
        } else {
          imgDescricao!.innerHTML = imgDesc;
        }
        break;
      case 'Data':
        if (this.Ordem == 'asc') {
          imgData!.innerHTML = imgAsc;
        } else {
          imgData!.innerHTML = imgDesc;
        }
        break;
      case 'Complementos':
        if (this.Ordem == 'asc') {
          imgComplementos!.innerHTML = imgAsc;
        } else {
          imgComplementos!.innerHTML = imgDesc;
        }
        break;
      default:
        break;
      }
  }

  paginarAndamentos(): void {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<AndamentoProcesso>[] = [];
    let pagina = 1;
    this.Processo.Andamentos!.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(item => item.Pagina == this.DadosPaginados.Pagina).map(item => item.Item);
  }

  receiveMessageDadosPaginador($event: DadosPaginador) {
    this.DadosPaginador = $event
    this.paginarAndamentos();
  }

  exibirComplementos(processoId: number){}
}
