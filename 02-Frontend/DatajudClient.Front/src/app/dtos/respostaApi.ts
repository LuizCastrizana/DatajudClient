export interface RespostaApi<T> {
  Dados: T;
  Mensagem: string;
  Erros: string[];
}
