import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidacaoService {

  constructor() { }

  apenasNumeros(event: any): boolean {
    const pattern = /[0-9]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar))
      return false;
    else
      return true;
  }

  apenasNumerosPositivos(event: any): boolean {
    const pattern = /[1-9]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar))
      return false;
    else
      return true;
  }

}
