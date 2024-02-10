export interface CreateProcessoDto {
  numeroProcesso: string;
  nomeCaso: string;
  vara: string | null;
  comarca: string;
  observacao: string | null;
  estadoId: number;
  tribunalId: number;
}
