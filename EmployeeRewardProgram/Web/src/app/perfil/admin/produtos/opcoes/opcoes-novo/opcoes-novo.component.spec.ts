import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpcoesNovoComponent } from './opcoes-novo.component';

describe('OpcoesNovoComponent', () => {
  let component: OpcoesNovoComponent;
  let fixture: ComponentFixture<OpcoesNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpcoesNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpcoesNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
