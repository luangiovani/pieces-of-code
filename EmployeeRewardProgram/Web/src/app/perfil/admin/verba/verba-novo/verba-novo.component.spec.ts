import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerbaNovoComponent } from './verba-novo.component';

describe('VerbaNovoComponent', () => {
  let component: VerbaNovoComponent;
  let fixture: ComponentFixture<VerbaNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerbaNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerbaNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
