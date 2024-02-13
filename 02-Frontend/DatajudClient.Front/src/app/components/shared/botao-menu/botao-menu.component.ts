
import { Component, Input } from '@angular/core';
import { ItemMenu } from '../../../interfaces/shared/itemMenu';

@Component({
  selector: 'app-botao-menu',
  templateUrl: './botao-menu.component.html',
  styleUrl: './botao-menu.component.css'
})
export class BotaoMenuComponent {
  @Input() itemMenu: ItemMenu = {
    nome: '',
    link: '',
    icone: '',
  };
}
