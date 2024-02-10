import { DOCUMENT } from '@angular/common';
import { Component, HostListener, Inject } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';


@Component({
  selector: 'app-cabecalho',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
  templateUrl: './cabecalho.component.html',
  styleUrl: './cabecalho.component.css'
})
export class CabecalhoComponent {

  constructor(@Inject(DOCUMENT) document: Document) { }

  isClickMenu: boolean = false;

  clickMenu() {
    this.isClickMenu = true;
    let menu = document.getElementById("dropdown-content");
    if (menu != null )
      {
      if (menu.style.display == "none" || menu.style.display == "") {
        menu.style.display = "block";
      } else {
        menu.style.display = "none";
      }
    }
  }

  @HostListener("document:click", ["$event"])
  click() {
    let menu = document.getElementById("dropdown-content");
    if (!this.isClickMenu && menu?.style.display == "block") {
      menu.style.display = "none";
    }
    this.isClickMenu = false;
  }

}
