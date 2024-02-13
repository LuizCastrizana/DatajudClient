import { Component } from '@angular/core';
import { ItemMenu } from '../../interfaces/shared/itemMenu';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
})
export class HomeComponent {
  listaItens: ItemMenu[] = [
    {
      nome: 'Processos',
      link: 'processos',
      icone: 'processo-icon.png'
    },
    {
      nome: 'Tribunais',
      link: 'tribunais',
      icone: 'tribunal-icon.png'
    },
  ];

  ngOnInit(): void {
    this.listaItens.sort((a, b) => a.nome.localeCompare(b.nome));
  }
}
