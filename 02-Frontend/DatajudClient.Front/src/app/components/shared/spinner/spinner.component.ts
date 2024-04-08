import { Component, Inject, Injectable, Input, input } from '@angular/core';
import { DOCUMENT

 } from '@angular/common';
@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.css'
})
@Injectable({
  providedIn: 'root'
})
export class SpinnerComponent {

  private _id: string = '';

  // @Input() set id(value: string) {
  //   this._id = value;
  // }
  // get id(): string {
  //   return this._id;
  // }
  @Input() id: string | undefined = '';

  constructor( @Inject(DOCUMENT) private document: Document ) { }

  exibirSpinner(id: string) {
    let spinner = this.document.getElementById(id) as HTMLDivElement;
    if (spinner != null) {
      spinner.style.display = 'inline-block';
    }
  }

  oculatarSpinner(id: string) {
    let spinner = this.document.getElementById(id) as HTMLDivElement;
    if (spinner != null) {
      spinner.style.display = 'none';
    }
  }

}
