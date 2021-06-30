import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoRecomendacaoComponent } from './situacao-recomendacao.component';

describe('SituacaoRecomendacaoComponent', () => {
  let component: SituacaoRecomendacaoComponent;
  let fixture: ComponentFixture<SituacaoRecomendacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoRecomendacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoRecomendacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
