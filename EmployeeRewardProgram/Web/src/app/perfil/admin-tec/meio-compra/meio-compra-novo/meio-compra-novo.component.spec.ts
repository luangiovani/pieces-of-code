import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeioCompraNovoComponent } from './meio-compra-novo.component';

describe('MeioCompraNovoComponent', () => {
  let component: MeioCompraNovoComponent;
  let fixture: ComponentFixture<MeioCompraNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeioCompraNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeioCompraNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
