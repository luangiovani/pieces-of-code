import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrocarPontosComponent } from './trocar-pontos.component';

describe('TrocarPontosComponent', () => {
  let component: TrocarPontosComponent;
  let fixture: ComponentFixture<TrocarPontosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrocarPontosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrocarPontosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
