import { Component, Input } from '@angular/core';
import { TipoFeedbackEnum } from '../../../enums/tipoFeedbackEnum';
import { DadosFeedbackPopUp } from '../../../interfaces/shared/dadosFeedbackPopUp';
import { FeedbackService } from '../../../services/shared/feedback.service';

@Component({
  selector: 'app-feedback-popup',
  templateUrl: './feedback-popup.component.html',
  styleUrl: './feedback-popup.component.css'
})
export class FeedbackPopupComponent {

  @Input() DadosFeedbackPopUp: DadosFeedbackPopUp = {
    Id: "",
    TipoFeedback: TipoFeedbackEnum.Sucesso,
    Titulo: "",
    Mensagem: ""
  };
  constructor(private feedbackService: FeedbackService) { }

  tratarTipoFeedback() {
    switch (this.DadosFeedbackPopUp.TipoFeedback) {
      case TipoFeedbackEnum.Sucesso:
        return "popup-sucesso";
      case TipoFeedbackEnum.Erro:
        return "popup-erro";
      case TipoFeedbackEnum.Atencao:
        return "popup-atencao";
      default:
        return "popup-sucesso";
    }
  }

  ocultar(Id: string) {
    this.feedbackService.ocultarFeedbackPopUp(Id);
  }

}
