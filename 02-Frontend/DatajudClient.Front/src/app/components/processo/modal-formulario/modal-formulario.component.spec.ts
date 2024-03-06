import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalFormularioProcessoComponent } from './modal-formulario.component';

describe('ModalFormularioComponent', () => {
  let component: ModalFormularioProcessoComponent;
  let fixture: ComponentFixture<ModalFormularioProcessoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalFormularioProcessoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalFormularioProcessoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
