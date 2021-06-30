import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LojasNovoComponent } from './lojas-novo.component';

describe('LojasNovoComponent', () => {
  let component: LojasNovoComponent;
  let fixture: ComponentFixture<LojasNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LojasNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LojasNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
