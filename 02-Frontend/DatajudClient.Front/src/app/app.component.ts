import { Component } from '@angular/core';
import { DadosFeedbackPopUp } from './interfaces/shared/dadosFeedbackPopUp';
import { FeedbackService } from '../app/services/shared/feedback.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
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
