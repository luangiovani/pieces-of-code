import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioQtdePontosComponent } from './qtde-pontos.component';

describe('QtdePontosComponent', () => {
  let component: RelatorioQtdePontosComponent;
  let fixture: ComponentFixture<RelatorioQtdePontosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioQtdePontosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioQtdePontosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
