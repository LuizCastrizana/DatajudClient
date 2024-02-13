export interface CreateProcessoDto {
  NumeroProcesso: string;
  NomeCaso: string;
  Vara: string | null;
  Comarca: string;
  Observacao: string | null;
  EstadoId: number;
  TribunalId: number;
}
