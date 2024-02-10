import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelProcessosComponent } from './painel-processos.component';

describe('PainelProcessosComponent', () => {
  let component: PainelProcessosComponent;
  let fixture: ComponentFixture<PainelProcessosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PainelProcessosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PainelProcessosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
