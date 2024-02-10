import { HttpClientModule, HttpClient } from '@angular/common/http';
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

  private readonly API = environment.datajudClientApiAdress + "/api/Processo";

  listar(): Observable<RespostaApi<ReadProcessoDto[]>> {
    return this.http.get<RespostaApi<ReadProcessoDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadProcessoDto[]>> {
    const url = `${this.API}/?busca=${busca}`
    return this.http.get<RespostaApi<ReadProcessoDto[]>>(url);
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadProcessoDto>>(url)
  }

  incluir (CreateProcessoDto: CreateProcessoDto): Observable<RespostaApi<ReadProcessoDto>> {
    return this.http.post<RespostaApi<ReadProcessoDto>>(this.API, CreateProcessoDto)
  }

  atualizar (id: string, UpdateProcessoDto: UpdateProcessoDto): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadProcessoDto>>(url, UpdateProcessoDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadProcessoDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadProcessoDto>>(url)
  }
}
