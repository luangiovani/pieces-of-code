import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheRecomendacaoComponent } from './detalhe-recomendacao.component';

describe('DetalheRecomendacaoComponent', () => {
  let component: DetalheRecomendacaoComponent;
  let fixture: ComponentFixture<DetalheRecomendacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalheRecomendacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalheRecomendacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
