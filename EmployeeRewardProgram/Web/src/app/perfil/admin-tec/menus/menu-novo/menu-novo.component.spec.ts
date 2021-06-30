import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuNovoComponent } from './menu-novo.component';

describe('MenuNovoComponent', () => {
  let component: MenuNovoComponent;
  let fixture: ComponentFixture<MenuNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MenuNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
