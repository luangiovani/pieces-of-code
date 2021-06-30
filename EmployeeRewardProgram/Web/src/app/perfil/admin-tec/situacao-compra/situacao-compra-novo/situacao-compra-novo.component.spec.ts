import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoCompraNovoComponent } from './situacao-compra-novo.component';

describe('SituacaoCompraNovoComponent', () => {
  let component: SituacaoCompraNovoComponent;
  let fixture: ComponentFixture<SituacaoCompraNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoCompraNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoCompraNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
