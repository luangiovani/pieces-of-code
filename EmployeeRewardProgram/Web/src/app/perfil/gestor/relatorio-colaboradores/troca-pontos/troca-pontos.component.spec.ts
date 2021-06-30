import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioTrocaPontosComponent } from './troca-pontos.component';

describe('TrocaPontosComponent', () => {
  let component: RelatorioTrocaPontosComponent;
  let fixture: ComponentFixture<RelatorioTrocaPontosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioTrocaPontosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioTrocaPontosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
