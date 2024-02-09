import { Component } from '@angular/core';
import { ItemMenu } from '../../interfaces/shared/itemMenu';
import { BotaoMenuComponent } from "../shared/botao-menu/botao-menu.component";

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [BotaoMenuComponent]
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
