import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpcoesValoresNovoComponent } from './opcoes-valores-novo.component';

describe('OpcoesValoresNovoComponent', () => {
  let component: OpcoesValoresNovoComponent;
  let fixture: ComponentFixture<OpcoesValoresNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpcoesValoresNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpcoesValoresNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
