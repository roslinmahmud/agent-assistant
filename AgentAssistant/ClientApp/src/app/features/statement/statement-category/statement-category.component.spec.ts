import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatementCategoryComponent } from './statement-category.component';

describe('StatementCategoryComponent', () => {
  let component: StatementCategoryComponent;
  let fixture: ComponentFixture<StatementCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatementCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatementCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
