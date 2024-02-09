
import { Component, Input } from '@angular/core';
import { ItemMenu } from '../../../interfaces/shared/itemMenu';
import { RouterOutlet, RouterModule } from '@angular/router';

@Component({
  selector: 'app-botao-menu',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
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
