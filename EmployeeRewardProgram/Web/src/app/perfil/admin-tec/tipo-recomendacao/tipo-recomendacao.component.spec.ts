import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoRecomendacaoComponent } from './tipo-recomendacao.component';

describe('TipoRecomendacaoComponent', () => {
  let component: TipoRecomendacaoComponent;
  let fixture: ComponentFixture<TipoRecomendacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoRecomendacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoRecomendacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
