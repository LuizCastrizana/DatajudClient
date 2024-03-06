import { Injectable } from '@angular/core';
import { TipoFeedbackEnum } from '../../enums/tipoFeedbackEnum';
import { DadosFeedbackPopUp } from '../../interfaces/shared/dadosFeedbackPopUp';
import { FeedbackService } from './feedback.service';

@Injectable({
  providedIn: 'root'
})
export class RespostaApiService {

  constructor(
    private FeedbackService: FeedbackService,
  ) { }

  tratarRespostaApi(resposta: any) {
    let DadosFeedbackPopUp = {} as DadosFeedbackPopUp;
    switch(resposta.status) {
      case 200:
        DadosFeedbackPopUp = {
          Id: 'feedback',
          TipoFeedback: TipoFeedbackEnum.Sucesso,
          Titulo: 'Sucesso!',
          Mensagem: resposta.mensagem,
        } as DadosFeedbackPopUp;
        break;
      case 204:
        DadosFeedbackPopUp = {
          Id: 'feedback',
          TipoFeedback: TipoFeedbackEnum.Sucesso,
          Titulo: 'Sucesso!',
          Mensagem: '',
        } as DadosFeedbackPopUp;
        break;
      case 400:
        DadosFeedbackPopUp = {
          Id: 'feedback',
          TipoFeedback: TipoFeedbackEnum.Atencao,
          Titulo: 'Validação rejeitada!',
          Mensagem: 'Erros: ' + resposta.error.erros.join(', ')
        } as DadosFeedbackPopUp;
        break;
      case 404:
        DadosFeedbackPopUp = {
          Id: 'feedback',
          TipoFeedback: TipoFeedbackEnum.Erro,
          Titulo: 'Erro!',
          Mensagem: 'Recurso não encontrado.',
        } as DadosFeedbackPopUp;
        break;
      case 500:
        DadosFeedbackPopUp = {
          Id: 'feedback',
          TipoFeedback: TipoFeedbackEnum.Erro,
          Titulo: 'Erro!',
          Mensagem: resposta.error.mensagem,
        };
        break;
        default:
          DadosFeedbackPopUp = {
            Id: 'feedback',
            TipoFeedback: TipoFeedbackEnum.Erro,
            Titulo: 'Erro!',
            Mensagem: 'Não foi possível completar a operação',
          } as DadosFeedbackPopUp;
          break;
      }
      this.FeedbackService.gerarFeedbackPopUp(DadosFeedbackPopUp);
  }
}
