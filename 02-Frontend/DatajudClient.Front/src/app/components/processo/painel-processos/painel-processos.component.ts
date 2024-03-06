import { UpdateProcessoDto } from './../../../dtos/processo/updateProcessoDto';
import { RespostaApiService } from './../../../services/shared/resposta-api.service';
import { Component, OnInit } from '@angular/core';
import { DadosPaginador } from '../../../interfaces/shared/paginacao/dadosPaginador';
import { Processo } from '../../../interfaces/processo/processo';
import { DadosPaginados } from '../../../interfaces/shared/paginacao/dadosPaginados';
import { ProcessoService } from '../../../services/processo/processo.service';
import { FeedbackService } from '../../../services/shared/feedback.service';
import { TipoFeedbackEnum } from '../../../enums/tipoFeedbackEnum';
import { DadosFeedbackPopUp } from '../../../interfaces/shared/dadosFeedbackPopUp';
import { ProcessoMapper } from '../../../mappers/processo/processoMapper';
import { ItemPagina } from '../../../interfaces/shared/paginacao/itemPagina';
import { DadosModalExcluir } from '../../../interfaces/shared/dadosModalExcluir';
import { CreateProcessoDto } from '../../../dtos/processo/createProcessoDto';

@Component({
  selector: 'app-painel-processos',
  templateUrl: './painel-processos.component.html',
  styleUrl: './painel-processos.component.css'
})
export class PainelProcessosComponent {

  Ordem: string = '';
  NomeCampo: string = '';

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 15,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Processo> = { } as DadosPaginados<Processo>;
  ProcessoAcao: Processo = { } as Processo;
  AcaoEditar: boolean = false;
  Processos: Processo[] = [];
  DadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;

  constructor(
    private processoService: ProcessoService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
  ) { }

  ngOnInit(): void {
    this.processoService.listar().subscribe({
      next: (respostaApi) => {
        respostaApi.dados.forEach(processoDto => {
          this.Processos.push(ProcessoMapper.FromDto(processoDto));
        });
        this.ordenarProcessos();
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoFeedback: TipoFeedbackEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter os processos."
        } as DadosFeedbackPopUp;
        this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
      }
    });
  }

  buscarProcessos(): void {
    let busca = (document.getElementById("txtBusca") as HTMLInputElement).value;
    this.Processos = [];
    this.processoService.buscar(busca).subscribe({
      next: (respostaApi) => {
        respostaApi.dados.forEach(processoDto => {
          this.Processos.push(ProcessoMapper.FromDto(processoDto));
        });
        this.ordenarProcessos();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
      }
    });
  }

  ordenarProcessos(nomeCampo?: string): void {
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
      case "NumeroProcesso":
        this.NomeCampo = "NumeroProcesso";
        if (this.Ordem == "asc") {
          this.Processos.sort((a, b) => a.numeroProcesso.localeCompare(b.numeroProcesso));
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => b.numeroProcesso.localeCompare(a.numeroProcesso));
        }
        break;
      case "NomeCaso":
        this.NomeCampo = "NomeCaso";
        if (this.Ordem == "asc") {
          this.Processos.sort((a, b) => a.nomeCaso.localeCompare(b.nomeCaso));
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => b.nomeCaso.localeCompare(a.nomeCaso));
        }
        break;
      case "Tribunal":
        this.NomeCampo = "Tribunal";
        if (this.Ordem == "asc") {
          this.Processos.sort((a, b) => a.tribunal.nome.localeCompare(b.tribunal.nome));
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => b.tribunal.nome.localeCompare(a.tribunal.nome));
        }
        break;
      case "UltimoAndamento":
        this.NomeCampo = "UltimoAndamento";
        if (this.Ordem == "asc") {
          this.Processos.sort((a, b) => a.ultimoAndamento.getTime() - b.ultimoAndamento.getTime());
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => b.ultimoAndamento.getTime() - a.ultimoAndamento.getTime());
        }
        break;
      case "UltimaAtualizacao":
        this.NomeCampo = "UltimaAtualizacao";
        if (this.Ordem == "asc") {
          this.Processos.sort((a, b) => a.ultimaAtualizacao.getTime() - b.ultimaAtualizacao.getTime());
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => b.ultimaAtualizacao.getTime() - a.ultimaAtualizacao.getTime());
        }
        break;
      default:
        this.NomeCampo = '';
        this.Processos.sort((a, b) => b.id - a.id);
        break;
    }
    this.tratarIconeOrdenacao();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgNumeroProcesso = document.getElementById('imgNumeroProcesso');
    let imgNomeCaso = document.getElementById('imgNomeCaso');
    let imgTribunal = document.getElementById('imgTribunal');
    let imgUltimoAndamento = document.getElementById('imgUltimoAndamento');
    let imgUltimaAtualizacao = document.getElementById('imgUltimaAtualizacao');

    imgNumeroProcesso!.innerHTML = '';
    imgNomeCaso!.innerHTML = '';
    imgTribunal!.innerHTML = '';
    imgUltimoAndamento!.innerHTML = '';
    imgUltimaAtualizacao!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'NumeroProcesso':
        if (this.Ordem == 'asc') {
          imgNumeroProcesso!.innerHTML = imgAsc;
        } else {
          imgNumeroProcesso!.innerHTML = imgDesc;
        }
        break;
      case 'NomeCaso':
        if (this.Ordem == 'asc') {
          imgNomeCaso!.innerHTML = imgAsc;
        } else {
          imgNomeCaso!.innerHTML = imgDesc;
        }
        break;
      case 'Tribunal':
        if (this.Ordem == 'asc') {
          imgTribunal!.innerHTML = imgAsc;
        }
        else {
          imgTribunal!.innerHTML = imgDesc;
        }
        break;
      case 'UltimoAndamento':
        if (this.Ordem == 'asc') {
          imgUltimoAndamento!.innerHTML = imgAsc;
        }
        else {
          imgUltimoAndamento!.innerHTML = imgDesc;
        }
        break;
      case 'UltimaAtualizacao':
        if (this.Ordem == 'asc') {
          imgUltimaAtualizacao!.innerHTML = imgAsc;
        }
        else {
          imgUltimaAtualizacao!.innerHTML = imgDesc;
        }
        break;
      default:
        break;
      }
  }

  receberDadosPaginador($event: DadosPaginador): void {
    this.DadosPaginador = $event
    this.paginarProcessos();
  }

  paginarProcessos() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<Processo>[] = [];
    let pagina = 1;
    this.Processos.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(item => item.Pagina == this.DadosPaginados.Pagina).map(item => item.Item);
  }

  exibirModalExcluir(processo: Processo): void {
    this.ProcessoAcao = processo;
    this.DadosModalExcluir = {
      NomeRegistro: "Processo nº: " + processo.numeroProcesso,
      IdRegistro: processo.id
    }
    document.getElementById('modalExcluir')!.style.display = 'block';
  }

  receiveMessageExcluir($event: DadosModalExcluir) {
    this.DadosModalExcluir = $event;
    this.processoService.excluir(ProcessoMapper.ToDeleteDto(this.ProcessoAcao)).subscribe({
      next: (retornoApi) => {
        this.respostaApiService.tratarRespostaApi(retornoApi);
        this.buscarProcessos();
        document.getElementById('modalExcluir')!.style.display = 'none';
        window.scroll(0,0);
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        document.getElementById('modalExcluir')!.style.display = 'none';
        window.scroll(0,0);FeedbackService
      }
    });
  }

  exibirModalFormulario(Edicao: boolean, ProcessoAcao?: Processo) {
    this.ProcessoAcao = ProcessoAcao != undefined ? ProcessoAcao : ({} as Processo);
    this.AcaoEditar = Edicao;
    document.getElementById('modalFormularioProcesso')!.style.display = 'block';
  }

  receiveMessageFormulario($eventEdicao: boolean) {
    if ($eventEdicao) {
      this.processoService
          .atualizarDados(
            this.ProcessoAcao!.id.toString(),
            ProcessoMapper.ToUpdateDto(this.ProcessoAcao!)
          )
          .subscribe({
            next: (result) => {
              this.respostaApiService.tratarRespostaApi(result);
              this.buscarProcessos();
            },
            error: (err) => {
              this.respostaApiService.tratarRespostaApi(err);
            }
          });
    } else {
      this.processoService
          .incluir(new Array<CreateProcessoDto>().concat(ProcessoMapper.ToCreateDto(this.ProcessoAcao!)))
          .subscribe({
            next: (result) => {
              this.respostaApiService.tratarRespostaApi(result);
              this.buscarProcessos();
            },
            error: (err) => {
              this.respostaApiService.tratarRespostaApi(err);
            }
          });
    }
    document.getElementById('modalFormularioProcesso')!.style.display = 'none';
    window.scroll(0,0);
  }

  atualizarProcesso(processo: Processo): void {
    let numeros: String[] = [];
    numeros.push(processo.numeroProcesso);
    let UpdateProcessoDto = { Numeros: numeros } as UpdateProcessoDto;
    this.processoService.atualizar(UpdateProcessoDto).subscribe({
      next: (respostaApi) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoFeedback: TipoFeedbackEnum.Sucesso,
          Titulo: "Sucesso!",
          Mensagem: "Processo atualizado com sucesso."
        } as DadosFeedbackPopUp;
        this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
        this.buscarProcessos();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
      }
    });
  }

  atualizarTodosProcessos(): void {
    let numeros = this.Processos.map(processo => processo.numeroProcesso);
    let UpdateProcessoDto = { Numeros: numeros } as UpdateProcessoDto;
    this.processoService.atualizar(UpdateProcessoDto).subscribe({
      next: (respostaApi) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoFeedback: TipoFeedbackEnum.Sucesso,
          Titulo: "Sucesso!",
          Mensagem: "Processos atualizados com sucesso."
        } as DadosFeedbackPopUp;
        this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
        this.buscarProcessos();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
      }
    });
  }

}
