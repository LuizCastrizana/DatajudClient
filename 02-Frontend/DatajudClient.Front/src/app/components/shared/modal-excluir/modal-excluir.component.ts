import { Component, EventEmitter, Input, Output, OnChanges } from '@angular/core';
import { DadosModalExcluir } from '../../../interfaces/shared/dadosModalExcluir';

@Component({
  selector: 'app-modal-excluir',
  templateUrl: './modal-excluir.component.html',
  styleUrl: './modal-excluir.component.css'
})
export class ModalExcluirComponent {

  private _dadosModalExcluir: DadosModalExcluir = {} as DadosModalExcluir;

  @Input() set DadosModalExcluir(value: DadosModalExcluir) {
    this._dadosModalExcluir = value;
    if (this.DadosModalExcluir.ExcluirMuitos) {
      this.DadosModalExcluir.NomeRegistro = "";
      this.mensagem = `Deseja realmente excluir os registros selecionados`;
    } else {
      this.mensagem = `Deseja realmente excluir o registro: `;
    }
  }
  get DadosModalExcluir(): DadosModalExcluir {
    return this._dadosModalExcluir;
  }

  @Output() ExcluirRegistro = new EventEmitter<DadosModalExcluir>();

  mensagem: string = '';

  constructor() { }

  excluirRegistro() {
    this.ExcluirRegistro.emit(this.DadosModalExcluir);
  }

  cancelar() {
    document.getElementById('modalExcluir')!.style.display = 'none';
  }

}
