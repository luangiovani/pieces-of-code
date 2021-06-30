import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurarExpiracaoComponent } from './configurar-expiracao.component';

describe('ConfigurarExpiracaoComponent', () => {
  let component: ConfigurarExpiracaoComponent;
  let fixture: ComponentFixture<ConfigurarExpiracaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurarExpiracaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurarExpiracaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
