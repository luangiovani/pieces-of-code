import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoRecomendacaoNovoComponent } from './tipo-recomendacao-novo.component';

describe('TipoRecomendacaoNovoComponent', () => {
  let component: TipoRecomendacaoNovoComponent;
  let fixture: ComponentFixture<TipoRecomendacaoNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoRecomendacaoNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoRecomendacaoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
