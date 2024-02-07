import { TipoFeedbackEnum } from "../../enums/tipoFeedbackEnum";

export interface DadosFeedbackPopUp {
    Id: string;
    TipoFeedback: TipoFeedbackEnum;
    Titulo: string;
    Mensagem: string;
}
