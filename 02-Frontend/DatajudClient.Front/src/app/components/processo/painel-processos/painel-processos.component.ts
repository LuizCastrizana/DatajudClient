import { UpdateProcessoDto } from './../../../dtos/processo/updateProcessoDto';
import { RespostaApiService } from './../../../services/shared/resposta-api.service';
import { Component, Inject, OnInit } from '@angular/core';
import { DadosPaginador } from '../../../interfaces/shared/paginacao/dadosPaginador';
import { Processo } from '../../../models/processo/processo';
import { DadosPaginados } from '../../../interfaces/shared/paginacao/dadosPaginados';
import { ProcessoService } from '../../../services/processo/processo.service';
import { FeedbackService } from '../../../services/shared/feedback.service';
import { TipoFeedbackEnum } from '../../../enums/tipoFeedbackEnum';
import { DadosFeedbackPopUp } from '../../../interfaces/shared/dadosFeedbackPopUp';
import { ProcessoMapper } from '../../../mappers/processo/processoMapper';
import { ItemPagina } from '../../../interfaces/shared/paginacao/itemPagina';
import { DadosModalExcluir } from '../../../interfaces/shared/dadosModalExcluir';
import { CreateProcessoDto } from '../../../dtos/processo/createProcessoDto';
import { DeleteProcessoDto } from '../../../dtos/processo/deleteProcessoDto';
import { DOCUMENT } from '@angular/common';
import { SpinnerComponent } from '../../shared/spinner/spinner.component';

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
    ItensPorPagina: 10,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Processo> = { } as DadosPaginados<Processo>;
  ProcessoAcao: Processo = { } as Processo;
  AcaoEditar: boolean = false;
  Processos: Processo[] = [];
  ProcessosSelecionados: Processo[] = [];
  DadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;

  constructor(
    private processoService: ProcessoService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
    private spinnerComponent: SpinnerComponent,
    @Inject(DOCUMENT) private document: Document
  ) { }

  ngOnInit(): void {
    this.processoService.listar().subscribe({
      next: (respostaApi) => {
        respostaApi.dados.forEach(processoDto => {
          this.Processos.push(ProcessoMapper.FromDto(processoDto));
        });
        this.ordenarProcessos();
        this.spinnerComponent.oculatarSpinner('spinner1');
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoFeedback: TipoFeedbackEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter os processos."
        } as DadosFeedbackPopUp;
        this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
        this.spinnerComponent.oculatarSpinner('spinner1');
      }
    });
    this.spinnerComponent.exibirSpinner('spinner1');
  }

  buscarProcessos(): void {
    let busca = (this.document.getElementById("txtBusca") as HTMLInputElement).value;
    this.Processos = [];
    this.ProcessosSelecionados = [];
    this.processoService.buscar(busca).subscribe({
      next: (respostaApi) => {
        respostaApi.dados.forEach(processoDto => {
          this.Processos.push(ProcessoMapper.FromDto(processoDto));
        });
        this.ordenarProcessos();
        this.spinnerComponent.oculatarSpinner('spinner1');
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
        this.spinnerComponent.oculatarSpinner('spinner1');
      }
    });
    this.spinnerComponent.exibirSpinner('spinner1');
  }

  selecionarProcesso($event: any, processo: Processo) {
    if ($event.target.checked) {
      this.ProcessosSelecionados.push(processo);
    } else {
      this.ProcessosSelecionados = this.ProcessosSelecionados.filter(p => p.id != processo.id);
    }
  }

  selecionarTodos($event: any) {
    if ($event.target.checked) {
      this.ProcessosSelecionados = this.Processos;
    } else {
      this.ProcessosSelecionados = [];
    }

    this.paginarProcessos();
  }

  processoSelecinado(processo: Processo): string {
    let existe = this.ProcessosSelecionados.find(p => p.id == processo.id) != undefined;
    return existe == true ? "checked" : "";
  }

  tratarCheckboxTodos() {
    let chkTodos = this.document.getElementById('chkTodos') as HTMLInputElement;

    if (this.DadosPaginados.Itens.every(processo => this.ProcessosSelecionados.find(p => p.id == processo.id) != undefined)) {
      chkTodos.checked = true;
    } else {
      chkTodos.checked = false;
    }
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
          this.Processos.sort((a, b) => new Date(a.ultimoAndamento).getTime() - new Date(b.ultimoAndamento).getTime());
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => new Date(b.ultimoAndamento).getTime() - new Date(a.ultimoAndamento).getTime());
        }
        break;
      case "UltimaAtualizacao":
        this.NomeCampo = "UltimaAtualizacao";
        if (this.Ordem == "asc") {
          this.Processos.sort((a, b) => new Date(a.ultimaAtualizacao).getTime() - new Date(b.ultimaAtualizacao).getTime());
        } else {
          this.Ordem = "desc";
          this.Processos.sort((a, b) => new Date(b.ultimaAtualizacao).getTime() - new Date(a.ultimaAtualizacao).getTime());
        }
        break;
      default:
        this.NomeCampo = '';
        this.Processos.sort((a, b) => b.id - a.id);
        break;
    }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.DadosPaginador.TotalItens = this.Processos.length;
    this.paginarProcessos();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgNumeroProcesso = this.document.getElementById('imgNumeroProcesso');
    let imgNomeCaso = this.document.getElementById('imgNomeCaso');
    let imgTribunal = this.document.getElementById('imgTribunal');
    let imgUltimoAndamento = this.document.getElementById('imgUltimoAndamento');
    let imgUltimaAtualizacao = this.document.getElementById('imgUltimaAtualizacao');

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

  receiveMessageDadosPaginador($event: DadosPaginador) {
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

    this.tratarCheckboxTodos();
  }

  exibirModalExcluir(processo?: Processo) {
    if (processo == undefined) {
      this.DadosModalExcluir = {
        NomeRegistro: "",
        IdRegistro: 0,
        ExcluirMuitos: true
      }
    } else {
        this.ProcessoAcao = processo;
        this.DadosModalExcluir = {
        NomeRegistro: "Processo nº: " + processo.getNumeroProcessoFormatado(),
        IdRegistro: processo.id,
        ExcluirMuitos: false
      }
    }
    this.document.getElementById('modalExcluir')!.style.display = 'block';
  }

  receiveMessageExcluir($event: DadosModalExcluir) {
    this.DadosModalExcluir = $event;
    if (this.DadosModalExcluir.ExcluirMuitos) {
      this.excluirSelecionados();
      this.document.getElementById('modalExcluir')!.style.display = 'none';
      window.scroll(0,0);
    } else {
      this.processoService.excluir(ProcessoMapper.ToDeleteDto(this.ProcessoAcao)).subscribe({
        next: (retornoApi) => {
          this.respostaApiService.tratarRespostaApi(retornoApi);
          this.buscarProcessos();
          this.document.getElementById('modalExcluir')!.style.display = 'none';
          window.scroll(0,0);
        },
        error: (err) => {
          this.respostaApiService.tratarRespostaApi(err);
          this.document.getElementById('modalExcluir')!.style.display = 'none';
          window.scroll(0,0);
        }
      });
    }
  }

  exibirModalFormulario(Edicao: boolean, ProcessoAcao?: Processo) {
    this.ProcessoAcao = ProcessoAcao != undefined ? ProcessoAcao : ({} as Processo);
    this.AcaoEditar = Edicao;
    this.document.getElementById('modalFormularioProcesso')!.style.display = 'block';
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
    this.document.getElementById('modalFormularioProcesso')!.style.display = 'none';
    window.scroll(0,0);
  }

  atualizarProcesso(processo: Processo) {
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
        this.spinnerComponent.oculatarSpinner('spinner1');
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
        this.buscarProcessos();
        this.spinnerComponent.oculatarSpinner('spinner1');
      }
    });
    this.spinnerComponent.exibirSpinner('spinner1');
  }

  atualizarSelecionados(): void {
    let numeros = this.ProcessosSelecionados.map(processo => processo.numeroProcesso);
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
        this.spinnerComponent.oculatarSpinner('spinner1');
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
        this.buscarProcessos();
        this.spinnerComponent.oculatarSpinner('spinner1');
      }
    });
    let dadosFeedback = {
      Id: "feedback1",
      TipoFeedback: TipoFeedbackEnum.Sucesso,
      Titulo: "Sucesso!",
      Mensagem: "Iniciando atualização dos processos."
    } as DadosFeedbackPopUp;
    this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
    this.spinnerComponent.exibirSpinner('spinner1');
  }

  excluirSelecionados(): void {
    let ids = this.ProcessosSelecionados.map(processo => processo.id);
    if (ids.length == 0) {
      let dadosFeedback = {
        Id: "feedback1",
        TipoFeedback: TipoFeedbackEnum.Atencao,
        Titulo: "Atenção!",
        Mensagem: "Nenhum processo selecionado para exclusão."
      } as DadosFeedbackPopUp;
      this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
      return;
    }
    let DeleteProcessoDto = { Ids: ids } as DeleteProcessoDto;
    this.processoService.excluir(DeleteProcessoDto).subscribe({
      next: (respostaApi) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoFeedback: TipoFeedbackEnum.Sucesso,
          Titulo: "Sucesso!",
          Mensagem: "Processos excluídos com sucesso."
        } as DadosFeedbackPopUp;
        this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
        this.buscarProcessos();
        this.spinnerComponent.oculatarSpinner('spinner1');
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err)
        this.buscarProcessos();
        this.spinnerComponent.oculatarSpinner('spinner1');
      }
    });
    let dadosFeedback = {
      Id: "feedback1",
      TipoFeedback: TipoFeedbackEnum.Sucesso,
      Titulo: "Sucesso!",
      Mensagem: "Iniciando exclusão dos processos."
    } as DadosFeedbackPopUp;
    this.feedbackService.gerarFeedbackPopUp(dadosFeedback);
    this.spinnerComponent.exibirSpinner('spinner1');
  }

}
