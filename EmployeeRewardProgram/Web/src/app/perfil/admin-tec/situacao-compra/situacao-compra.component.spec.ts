import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoCompraComponent } from './situacao-compra.component';

describe('SituacaoCompraComponent', () => {
  let component: SituacaoCompraComponent;
  let fixture: ComponentFixture<SituacaoCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
