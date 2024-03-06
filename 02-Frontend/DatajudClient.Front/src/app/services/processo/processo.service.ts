import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { RespostaApi } from '../../dtos/respostaApi';
import { ReadProcessoDto } from '../../dtos/processo/readProcessoDto';
import { UpdateProcessoDto } from './../../dtos/processo/updateProcessoDto';
import { CreateProcessoDto } from './../../dtos/processo/createProcessoDto';
import { UpdateDadosProcessoDto } from '../../dtos/processo/updateDadosProcessoDto';
import { DeleteProcessoDto } from '../../dtos/processo/deleteProcessoDto';

@Injectable({
  providedIn: 'root'
})
export class ProcessoService {

  constructor(private http: HttpClient) { }

  private readonly API = environment.datajudClientApiAdress + "/api/Processos";

  listar(): Observable<RespostaApi<ReadProcessoDto[]>> {
    const url = `${this.API}/ObterProcessos`
    return this.http.get<RespostaApi<ReadProcessoDto[]>>(url);
  }

  buscar(busca: string): Observable<RespostaApi<ReadProcessoDto[]>> {
    const url = `${this.API}/ObterProcessos/?busca=${busca}`
    return this.http.get<RespostaApi<ReadProcessoDto[]>>(url);
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/ObterProcesso/${id}`
    return this.http.get<RespostaApi<ReadProcessoDto>>(url)
  }

  incluir (CreateProcessoDtos: CreateProcessoDto[]): Observable<RespostaApi<ReadProcessoDto[]>> {
    const url = `${this.API}/CadastrarProcessos`
    return this.http.post<RespostaApi<ReadProcessoDto[]>>(url, CreateProcessoDtos)
  }

  atualizar (UpdateProcessoDto: UpdateProcessoDto): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/AtualizarProcessos`
    return this.http.post<RespostaApi<ReadProcessoDto>>(url, UpdateProcessoDto)
  }

  atualizarDados (id: string, UpdateDadosProcessoDto: UpdateDadosProcessoDto): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/AtualizarDadosProcesso/${id}`
    return this.http.put<RespostaApi<ReadProcessoDto>>(url, UpdateDadosProcessoDto)
  }

  excluir (DeleteProcessoDto: DeleteProcessoDto): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/ExcluirProcessos`
    return this.http.post<RespostaApi<ReadProcessoDto>>(url, DeleteProcessoDto)
  }
}
