export interface DadosPaginados<T> {
  Pagina: number;
  ItensPorPagina: number;
  TotalItens: number;
  Itens: T[];
}
