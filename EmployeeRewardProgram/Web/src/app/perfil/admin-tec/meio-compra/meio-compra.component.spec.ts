import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeioCompraComponent } from './meio-compra.component';

describe('MeioCompraComponent', () => {
  let component: MeioCompraComponent;
  let fixture: ComponentFixture<MeioCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeioCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeioCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
