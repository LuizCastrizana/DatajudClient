import { RespostaApiService } from './../../../services/shared/resposta-api.service';
import { Component } from '@angular/core';
import { DadosPaginador } from '../../../interfaces/shared/paginacao/dadosPaginador';
import { Processo } from '../../../interfaces/processo/processo';
import { DadosPaginados } from '../../../interfaces/shared/paginacao/dadosPaginados';
import { RouterOutlet, RouterModule } from '@angular/router';
import { ProcessoService } from '../../../services/processo/processo.service';
import { FeedbackService } from '../../../services/shared/feedback.service';
import { TipoFeedbackEnum } from '../../../enums/tipoFeedbackEnum';
import { DadosFeedbackPopUp } from '../../../interfaces/shared/dadosFeedbackPopUp';
import { ProcessoMapper } from '../../../mappers/processo/processoMapper';

@Component({
  selector: 'app-painel-processos',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
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
  Processos: Processo[] = [];

  constructor(
    private processoService: ProcessoService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
  ) { }

  ngOninit(): void {
    this.processoService.listar().subscribe({
      next: (respostaApi) => {
        respostaApi.valor.forEach(processoDto => {
          this.Processos.push(ProcessoMapper.FromDto(processoDto));
        });
        this.ordenarProcessos();
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoFeedback: TipoFeedbackEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter os Processos."
        } as DadosFeedbackPopUp;
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      }
    });
  }

  buscarProcessos(): void {

  }
  ordenarProcessos(nomeCampo?: string): void {
  }
  tratarIconeOrdenacao() {
  }
  receberDadosPaginador($event: DadosPaginador): void {
  }
  paginarProcessos() {
  }
  exibirModalExcluir(processo: Processo): void {
  }
}
