import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerTaxaConversaoComponent } from './ver-taxa-conversao.component';

describe('VerTaxaConversaoComponent', () => {
  let component: VerTaxaConversaoComponent;
  let fixture: ComponentFixture<VerTaxaConversaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerTaxaConversaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerTaxaConversaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
