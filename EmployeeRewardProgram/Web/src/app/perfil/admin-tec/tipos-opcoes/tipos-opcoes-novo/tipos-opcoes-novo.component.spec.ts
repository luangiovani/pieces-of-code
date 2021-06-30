import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TiposOpcoesNovoComponent } from './tipos-opcoes-novo.component';

describe('TiposOpcoesNovoComponent', () => {
  let component: TiposOpcoesNovoComponent;
  let fixture: ComponentFixture<TiposOpcoesNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TiposOpcoesNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TiposOpcoesNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
