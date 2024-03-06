import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { RespostaApi } from '../../dtos/respostaApi';
import { Observable } from 'rxjs';
import { ReadEstadoDto } from '../../dtos/endereco/readEstadoDto';

@Injectable({
  providedIn: 'root'
})
export class EstadoService {

  constructor(private http: HttpClient) { }

  private readonly API = environment.datajudClientApiAdress + "/api/Enderecos";

  listar(): Observable<RespostaApi<ReadEstadoDto[]>> {
    const url = `${this.API}/ObterEstados`
    return this.http.get<RespostaApi<ReadEstadoDto[]>>(url);
  }
}
