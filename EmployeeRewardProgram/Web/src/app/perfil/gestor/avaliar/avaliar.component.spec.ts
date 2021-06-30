import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AvaliarComponent } from './avaliar.component';

describe('AvaliarComponent', () => {
  let component: AvaliarComponent;
  let fixture: ComponentFixture<AvaliarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AvaliarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AvaliarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
