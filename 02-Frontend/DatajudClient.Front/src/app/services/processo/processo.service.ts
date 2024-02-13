import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { RespostaApi } from '../../dtos/respostaApi';
import { ReadProcessoDto } from '../../dtos/processo/readProcessoDto';
import { UpdateProcessoDto } from './../../dtos/processo/updateProcessoDto';
import { CreateProcessoDto } from './../../dtos/processo/createProcessoDto';

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
    return this.http.post<RespostaApi<ReadProcessoDto[]>>(this.API, CreateProcessoDtos)
  }

  atualizar (UpdateProcessoDto: UpdateProcessoDto): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/AtualizarProcessos`
    return this.http.post<RespostaApi<ReadProcessoDto>>(url, UpdateProcessoDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/ExcluirProcesso/${id}`
    return this.http.delete<RespostaApi<ReadProcessoDto>>(url)
  }
}
