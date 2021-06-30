import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EfetivarVendaComponent } from './efetivar-venda.component';

describe('EfetivarVendaComponent', () => {
  let component: EfetivarVendaComponent;
  let fixture: ComponentFixture<EfetivarVendaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EfetivarVendaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EfetivarVendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
