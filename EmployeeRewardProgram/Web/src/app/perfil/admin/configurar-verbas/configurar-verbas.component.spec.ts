import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurarVerbasComponent } from './configurar-verbas.component';

describe('ConfigurarVerbasComponent', () => {
  let component: ConfigurarVerbasComponent;
  let fixture: ComponentFixture<ConfigurarVerbasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurarVerbasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurarVerbasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
