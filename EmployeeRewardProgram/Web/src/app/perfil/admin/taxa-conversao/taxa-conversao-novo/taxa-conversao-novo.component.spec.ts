import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxaConversaoNovoComponent } from './taxa-conversao-novo.component';

describe('TaxaConversaoNovoComponent', () => {
  let component: TaxaConversaoNovoComponent;
  let fixture: ComponentFixture<TaxaConversaoNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaxaConversaoNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaxaConversaoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
