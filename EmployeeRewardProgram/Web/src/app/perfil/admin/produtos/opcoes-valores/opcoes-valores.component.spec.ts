import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpcoesValoresComponent } from './opcoes-valores.component';

describe('OpcoesValoresComponent', () => {
  let component: OpcoesValoresComponent;
  let fixture: ComponentFixture<OpcoesValoresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpcoesValoresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpcoesValoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
