import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoComponent } from './aplicacao.component';

describe('AplicacaoComponent', () => {
  let component: AplicacaoComponent;
  let fixture: ComponentFixture<AplicacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AplicacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AplicacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
