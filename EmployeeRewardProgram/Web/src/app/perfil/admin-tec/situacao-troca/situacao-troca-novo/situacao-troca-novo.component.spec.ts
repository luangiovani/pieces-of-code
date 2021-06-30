import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoTrocaNovoComponent } from './situacao-troca-novo.component';

describe('SituacaoTrocaNovoComponent', () => {
  let component: SituacaoTrocaNovoComponent;
  let fixture: ComponentFixture<SituacaoTrocaNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoTrocaNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoTrocaNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
