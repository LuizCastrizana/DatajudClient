export class StringTools {
  static apenasNumeros(texto: string): string {
    return texto.replace(/\D/g, '');
}
}
