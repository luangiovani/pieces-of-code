import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MinhasTrocasComponent } from './minhas-trocas.component';

describe('MinhasTrocasComponent', () => {
  let component: MinhasTrocasComponent;
  let fixture: ComponentFixture<MinhasTrocasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MinhasTrocasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MinhasTrocasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
