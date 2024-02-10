import { Routes } from '@angular/router';
import { NgModule} from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { IncluirProcessoComponent } from './components/processo/incluir-processo/incluir-processo.component';
import { PainelProcessosComponent } from './components/processo/painel-processos/painel-processos.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'processos',
    component: PainelProcessosComponent
  },
  {
    path: 'incluir-processo',
    component: IncluirProcessoComponent
  }
];

@NgModule({
  imports: [],
  exports: []
})
export class AppRoutingModule { }
