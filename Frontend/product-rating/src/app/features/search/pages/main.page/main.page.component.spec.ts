import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Main.PageComponent } from './main.page.component';

describe('Main.PageComponent', () => {
  let component: Main.PageComponent;
  let fixture: ComponentFixture<Main.PageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Main.PageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Main.PageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
