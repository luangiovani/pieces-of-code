import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxaConversaoComponent } from './taxa-conversao.component';

describe('TaxaConversaoComponent', () => {
  let component: TaxaConversaoComponent;
  let fixture: ComponentFixture<TaxaConversaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaxaConversaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaxaConversaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
