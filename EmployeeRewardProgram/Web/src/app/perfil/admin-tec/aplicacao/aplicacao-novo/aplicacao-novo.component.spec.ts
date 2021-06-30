import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoNovoComponent } from './aplicacao-novo.component';

describe('AplicacaoNovoComponent', () => {
  let component: AplicacaoNovoComponent;
  let fixture: ComponentFixture<AplicacaoNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AplicacaoNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AplicacaoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
