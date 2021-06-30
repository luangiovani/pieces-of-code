import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReconhecerComponent } from './reconhecer.component';

describe('ReconhecerComponent', () => {
  let component: ReconhecerComponent;
  let fixture: ComponentFixture<ReconhecerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReconhecerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReconhecerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
