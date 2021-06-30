import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoAvaliacaoNovoComponent } from './situacao-avaliacao-novo.component';

describe('SituacaoAvaliacaoNovoComponent', () => {
  let component: SituacaoAvaliacaoNovoComponent;
  let fixture: ComponentFixture<SituacaoAvaliacaoNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoAvaliacaoNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoAvaliacaoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
