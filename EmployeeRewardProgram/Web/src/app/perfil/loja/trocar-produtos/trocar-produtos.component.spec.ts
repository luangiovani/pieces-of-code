import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrocarProdutosComponent } from './trocar-produtos.component';

describe('TrocarProdutosComponent', () => {
  let component: TrocarProdutosComponent;
  let fixture: ComponentFixture<TrocarProdutosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrocarProdutosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrocarProdutosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
