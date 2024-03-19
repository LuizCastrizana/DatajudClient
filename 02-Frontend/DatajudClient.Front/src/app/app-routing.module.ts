import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PainelProcessosComponent } from './components/processo/painel-processos/painel-processos.component';
import { DetalhesProcessoComponent } from './components/processo/detalhes-processo/detalhes-processo.component';

const routes: Routes = [
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
    path: 'processos/detalhes-processo/:id',
    component: DetalhesProcessoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
