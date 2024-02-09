import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterModule } from '@angular/router';
import { CabecalhoComponent } from "./components/shared/cabecalho/cabecalho.component";
import { RodapeComponent } from "./components/shared/rodape/rodape.component";
import { FeedbackPopupComponent } from "./components/shared/feedback-popup/feedback-popup.component";
import { DadosFeedbackPopUp } from './interfaces/shared/dadosFeedbackPopUp';
import { FeedbackService } from '../app/services/shared/feedback.service';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, RouterModule, CabecalhoComponent, RodapeComponent, FeedbackPopupComponent]
})
export class AppComponent {
  title = 'DatajudClient.Front';
  DadosFeedbackPopUp: DadosFeedbackPopUp[] = [];

  constructor(
    private FeedbackService: FeedbackService
  ) { }

  ngOnInit(): void {
    this.FeedbackService.FeedbackAlertaEmitter.subscribe({
      next: (dadosFeedback: DadosFeedbackPopUp) => {
        dadosFeedback.Id = "feedback" + this.DadosFeedbackPopUp.length;
        this.DadosFeedbackPopUp.push(dadosFeedback);
      }
    });
    this.FeedbackService.FecharAletaEmitter.subscribe({
      next: (Id: string) => {
        this.DadosFeedbackPopUp = this.DadosFeedbackPopUp.filter(x=>x.Id != Id);
      }
    });
  }
}
