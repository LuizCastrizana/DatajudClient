import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DadosModalExcluir } from '../../../interfaces/shared/dadosModalExcluir';

@Component({
  selector: 'app-modal-excluir',
  templateUrl: './modal-excluir.component.html',
  styleUrl: './modal-excluir.component.css'
})
export class ModalExcluirComponent {

  @Input() DadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;
  @Output() ExcluirRegistro = new EventEmitter<DadosModalExcluir>();

  constructor() { }

  excluirRegistro() {
    this.ExcluirRegistro.emit(this.DadosModalExcluir);
  }

  cancelar() {
    document.getElementById('modalExcluir')!.style.display = 'none';
  }

}
