import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalComplementosComponent } from './modal-complementos.component';

describe('ModalComplementosComponent', () => {
  let component: ModalComplementosComponent;
  let fixture: ComponentFixture<ModalComplementosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModalComplementosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModalComplementosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
