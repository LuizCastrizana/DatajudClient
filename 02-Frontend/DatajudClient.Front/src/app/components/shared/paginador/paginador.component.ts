import { ValidacaoService } from '../../../services/shared/validacao.service';
import { DadosPaginador } from '../../../interfaces/shared/paginacao/dadosPaginador';
import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-paginador',
  templateUrl: './paginador.component.html',
  styleUrls: ['./paginador.component.css']
})
export class PaginadorComponent implements OnInit {
  @Input() DadosPaginador: DadosPaginador = {
    TotalItens: 0,
    ItensPorPagina: 0,
    PaginaAtual: 0
  };

  constructor(private serviceValidacao: ValidacaoService) { }

  @Output() messageEvent = new EventEmitter<DadosPaginador>();

  ngOnInit(): void {
  }

  goToPage(pagina?: number) {
    if (pagina != undefined) {
      this.DadosPaginador.PaginaAtual = pagina;
    }
    if (this.DadosPaginador.PaginaAtual > (this.DadosPaginador.TotalItens / this.DadosPaginador.ItensPorPagina) + 1)
      this.DadosPaginador.PaginaAtual = ((this.DadosPaginador.TotalItens - (this.DadosPaginador.TotalItens % this.DadosPaginador.ItensPorPagina)) / this.DadosPaginador.ItensPorPagina) + 1;
    if (this.DadosPaginador.PaginaAtual < 1)
      this.DadosPaginador.PaginaAtual = 1;
    this.sendMessege()
  }

  goToNextPage() {
    if (this.DadosPaginador.PaginaAtual < this.DadosPaginador.TotalItens / this.DadosPaginador.ItensPorPagina)
      this.DadosPaginador.PaginaAtual++;
    this.sendMessege()
  }

  goToPreviousPage() {
    if (this.DadosPaginador.PaginaAtual > 1)
      this.DadosPaginador.PaginaAtual--;
    this.sendMessege()
  }

  sendMessege() {
    this.messageEvent.emit(this.DadosPaginador);
  }

  apenasNumeros(event: any) {
    if (!this.serviceValidacao.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

}
