import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RealizarVendaComponent } from './realizar-venda.component';

describe('RealizarVendaComponent', () => {
  let component: RealizarVendaComponent;
  let fixture: ComponentFixture<RealizarVendaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RealizarVendaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RealizarVendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
