import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoRecomendacaoNovoComponent } from './situacao-recomendacao-novo.component';

describe('SituacaoRecomendacaoNovoComponent', () => {
  let component: SituacaoRecomendacaoNovoComponent;
  let fixture: ComponentFixture<SituacaoRecomendacaoNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoRecomendacaoNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoRecomendacaoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
