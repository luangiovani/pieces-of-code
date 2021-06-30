import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoAvaliacaoComponent } from './situacao-avaliacao.component';

describe('SituacaoAvaliacaoComponent', () => {
  let component: SituacaoAvaliacaoComponent;
  let fixture: ComponentFixture<SituacaoAvaliacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoAvaliacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoAvaliacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
