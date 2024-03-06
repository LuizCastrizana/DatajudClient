export interface RespostaApi<T> {
  dados: T;
  mensagem: string;
  erros: string[];
  status: number;
}
