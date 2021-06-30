import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeusPontosComponent } from './meus-pontos.component';

describe('MeusPontosComponent', () => {
  let component: MeusPontosComponent;
  let fixture: ComponentFixture<MeusPontosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeusPontosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeusPontosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
