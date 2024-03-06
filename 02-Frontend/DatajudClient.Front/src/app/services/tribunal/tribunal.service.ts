import { Tribunal } from './../../interfaces/tribunal/tribunal';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { RespostaApi } from '../../dtos/respostaApi';
import { ReadTribunalDto } from '../../dtos/tribunal/readTribunalDto';
import { UpdateTribunalDto } from './../../dtos/tribunal/updateTribunalDto';

@Injectable({
  providedIn: 'root'
})
export class TribunalService {

  constructor(private http: HttpClient) { }

  private readonly API = environment.datajudClientApiAdress + "/api/Tribunais";

  listar(): Observable<RespostaApi<ReadTribunalDto[]>> {
    const url = `${this.API}/ObterTribunais`
    return this.http.get<RespostaApi<ReadTribunalDto[]>>(url);
  }

  buscar(busca: string): Observable<RespostaApi<ReadTribunalDto[]>> {
    const url = `${this.API}/ObterTribunais/?busca=${busca}`
    return this.http.get<RespostaApi<ReadTribunalDto[]>>(url);
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadTribunalDto>> {
    const url = `${this.API}/ObterTribunal/${id}`
    return this.http.get<RespostaApi<ReadTribunalDto>>(url)
  }
}
