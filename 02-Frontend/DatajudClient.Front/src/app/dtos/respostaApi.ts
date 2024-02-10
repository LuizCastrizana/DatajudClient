export interface RespostaApi<T> {
  valor: T;
  mensagem: string;
  erros: string[];
}
