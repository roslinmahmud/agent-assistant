import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VoltListComponent } from './volt-list.component';

describe('VoltListComponent', () => {
  let component: VoltListComponent;
  let fixture: ComponentFixture<VoltListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ VoltListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoltListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
