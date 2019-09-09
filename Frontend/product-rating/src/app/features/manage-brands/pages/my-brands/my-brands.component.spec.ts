import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBrandsComponent } from './my-brands.component';

describe('MyBrandsComponent', () => {
  let component: MyBrandsComponent;
  let fixture: ComponentFixture<MyBrandsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyBrandsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyBrandsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
