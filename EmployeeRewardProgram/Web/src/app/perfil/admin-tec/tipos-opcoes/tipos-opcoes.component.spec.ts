import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TiposOpcoesComponent } from './tipos-opcoes.component';

describe('TiposOpcoesComponent', () => {
  let component: TiposOpcoesComponent;
  let fixture: ComponentFixture<TiposOpcoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TiposOpcoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TiposOpcoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
