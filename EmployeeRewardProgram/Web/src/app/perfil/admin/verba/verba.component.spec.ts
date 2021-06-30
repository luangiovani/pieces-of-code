import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerbaComponent } from './verba.component';

describe('VerbaComponent', () => {
  let component: VerbaComponent;
  let fixture: ComponentFixture<VerbaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerbaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerbaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
