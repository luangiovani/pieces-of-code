import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricoTrocasComponent } from './historico-trocas.component';

describe('HistoricoTrocasComponent', () => {
  let component: HistoricoTrocasComponent;
  let fixture: ComponentFixture<HistoricoTrocasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricoTrocasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricoTrocasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
