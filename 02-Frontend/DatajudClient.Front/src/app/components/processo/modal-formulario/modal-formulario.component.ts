import { Tribunal } from './../../../interfaces/tribunal/tribunal';
import { EstadoMapper } from './../../../mappers/endereco/estadoMapper';
import { EstadoService } from './../../../services/endereco/estado.service';
import { Estado } from './../../../interfaces/endereco/estado';
import { TribunalMapper } from './../../../mappers/tribunal/tribunalMapper';
import { TribunalService } from './../../../services/tribunal/tribunal.service';
import { Component, EventEmitter, Input, Output, input } from '@angular/core';
import { Router } from '@angular/router';
import { IProcesso } from '../../../interfaces/processo/processo';
import { ValidacaoService } from '../../../services/shared/validacao.service';
import { RespostaApiService } from '../../../services/shared/resposta-api.service';

@Component({
  selector: 'app-modal-formulario-processo',
  templateUrl: './modal-formulario.component.html',
  styleUrl: './modal-formulario.component.css'
})
export class ModalFormularioProcessoComponent {

  constructor(
    private validacaoService: ValidacaoService,
    private tribunalService: TribunalService,
    private estadoService: EstadoService,
    private respostaApiService: RespostaApiService,
    private router: Router
  ) {}

  @Input() Processo: IProcesso = {} as IProcesso;
  @Output() ProcessoChange = new EventEmitter<IProcesso>();
  @Input() Edicao: boolean = false;
  @Output() EventoEdicao = new EventEmitter<boolean>();

  TribunalId: number = 0;
  Tribunais: Tribunal[] = [];

  EstadoId: number = 0;
  Estados: Estado[] = [];

  ngOnInit(): void {
    this.obterListasTribunalEstado();
  }

  ngOnChanges() {
    if (this.Processo.tribunal != undefined) {
      this.TribunalId = this.Processo.tribunal.id;
    }
    if (this.Processo.estado != undefined) {
      this.EstadoId = this.Processo.estado.id;
    }
  }

  obterListasTribunalEstado() {
    this.tribunalService.listar().subscribe({
      next: (result) => {
        result.dados.forEach(tribunal => {
          this.Tribunais.push(TribunalMapper.FromDto(tribunal));
        });
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi( { status: 500, error: { mensagem: 'Não foi possível obter a lista de tribunais.' } } );
      },
    });

    this.estadoService.listar().subscribe({
      next: (result) => {
        result.dados.forEach(estado => {
          this.Estados.push(EstadoMapper.FromDto(estado));
        });
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi( { status: 500, error: { mensagem: 'Não foi possível obter a lista de estados.' } } );
      },
    });
  }

  salvarDados(){
    if (this.validarCamposObrigatorios()) {
      this.Processo.tribunal = this.Tribunais.find(x => x.id == this.TribunalId)!;
      this.Processo.estado = this.Estados.find(x => x.id == this.EstadoId)!;
      this.EventoEdicao.emit(this.Edicao);
    }
  }

  fechar() {
    document.getElementById('modalFormularioProcesso')!.style.display = 'none';
  }

  //#region Validações

  validarCamposObrigatorios(): boolean {
    let erros: number = 0;
    let camposObrigatorios = [
      'txtNumeroProcesso',
      'txtNomeCaso',
      'selTribunalId',
      'selEstadoOrigemId',
      'txtVara',
      'txtComarca'
    ];
    camposObrigatorios.forEach((campo) => {
      if (!this.campoObrigatorio(campo)) {
        erros++;
      }
    });
    return erros == 0 ? true : false;
  }

  campoObrigatorio(campoId: string): boolean {
    let campo = <HTMLInputElement>document.getElementById(campoId)!;
    let campoErro = document.getElementById('erro_' + campoId)!;
    if (
      campo.value === '' ||
      campo.value == null ||
      campo.value == undefined ||
      (campo.tagName == 'SELECT' && campo.value == '0')
    ) {
      campo.classList.add('campo-obrigatorio');
      campoErro.style.display = 'block';
      campoErro.innerHTML = 'Campo obrigatório';
      return false;
    } else {
      campoErro.style.display = 'none';
      campo.classList.remove('campo-obrigatorio');
      return true;
    }
  }

  apenasNumerosPositivos(event: any) {
    if (!this.validacaoService.apenasNumerosPositivos(event)) {
      event.preventDefault();
    }
  }

  apenasNumeros(event: any) {
    if (!this.validacaoService.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  //#endregion
}
