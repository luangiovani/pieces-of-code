import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrocasEfetuadasComponent } from './trocas-efetuadas.component';

describe('TrocasEfetuadasComponent', () => {
  let component: TrocasEfetuadasComponent;
  let fixture: ComponentFixture<TrocasEfetuadasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrocasEfetuadasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrocasEfetuadasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
