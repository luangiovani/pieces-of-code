import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OutraEquipeComponent } from './outra-equipe.component';

describe('OutraEquipeComponent', () => {
  let component: OutraEquipeComponent;
  let fixture: ComponentFixture<OutraEquipeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OutraEquipeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OutraEquipeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
