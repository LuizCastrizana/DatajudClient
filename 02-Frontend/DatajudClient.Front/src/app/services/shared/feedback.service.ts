import { EventEmitter, Injectable } from '@angular/core';
import { DadosFeedbackPopUp } from '../../interfaces/shared/dadosFeedbackPopUp';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  public FeedbackAlertaEmitter = new EventEmitter<DadosFeedbackPopUp>();
  public FecharAletaEmitter = new EventEmitter<string>();

  constructor() { }

  gerarFeedbackAlerta(Dados: DadosFeedbackPopUp) {
    this.FeedbackAlertaEmitter.emit(Dados);
  }

  ocultarFeedbackAlerta(Id: string) {
    this.FecharAletaEmitter.emit(Id);
  }
}
