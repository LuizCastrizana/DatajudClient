import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TipoFeedbackEnum } from '../../../enums/tipoFeedbackEnum';
import { DadosFeedbackPopUp } from '../../../interfaces/shared/dadosFeedbackPopUp';
import { FeedbackService } from '../../../services/shared/feedback.service';

@Component({
  selector: 'app-feedback-popup',
  standalone: true,
  imports: [CommonModule],
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
        return "alerta-sucesso";
      case TipoFeedbackEnum.Erro:
        return "alerta-erro";
      case TipoFeedbackEnum.Atencao:
        return "alerta-atencao";
      default:
        return "alerta-sucesso";
    }
  }

  ocultar(Id: string) {
    this.feedbackService.ocultarFeedbackAlerta(Id);
  }

}
