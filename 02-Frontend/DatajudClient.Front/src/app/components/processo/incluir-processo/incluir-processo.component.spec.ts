import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncluirProcessoComponent } from './incluir-processo.component';

describe('IncluirProcessoComponent', () => {
  let component: IncluirProcessoComponent;
  let fixture: ComponentFixture<IncluirProcessoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncluirProcessoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IncluirProcessoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
