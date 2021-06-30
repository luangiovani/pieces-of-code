import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoTrocaComponent } from './situacao-troca.component';

describe('SituacaoTrocaComponent', () => {
  let component: SituacaoTrocaComponent;
  let fixture: ComponentFixture<SituacaoTrocaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoTrocaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoTrocaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
