import { CommonModule } from '@angular/common';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { CabecalhoComponent } from "./components/shared/cabecalho/cabecalho.component";
import { RodapeComponent } from "./components/shared/rodape/rodape.component";
import { FeedbackPopupComponent } from "./components/shared/feedback-popup/feedback-popup.component";
import { ModalExcluirComponent } from './components/shared/modal-excluir/modal-excluir.component';
import { BotaoMenuComponent } from "./components/shared/botao-menu/botao-menu.component";
import { PaginadorComponent } from './components/shared/paginador/paginador.component';
import { HomeComponent } from './components/home/home.component';
import { PainelProcessosComponent } from './components/processo/painel-processos/painel-processos.component';
import { ModalFormularioProcessoComponent } from './components/processo/modal-formulario/modal-formulario.component';
import { DetalhesProcessoComponent } from './components/processo/detalhes-processo/detalhes-processo.component';

@NgModule({
  declarations: [
    AppComponent,
    CabecalhoComponent,
    RodapeComponent,
    FeedbackPopupComponent,
    ModalExcluirComponent,
    BotaoMenuComponent,
    PaginadorComponent,
    HomeComponent,
    PainelProcessosComponent,
    ModalFormularioProcessoComponent,
    DetalhesProcessoComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withFetch()),
    { provide: LOCALE_ID, useValue: 'pt-br' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
