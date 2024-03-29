import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryPickerComponent } from './category-picker.component';

describe('CategoryPickerComponent', () => {
  let component: CategoryPickerComponent;
  let fixture: ComponentFixture<CategoryPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoryPickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
