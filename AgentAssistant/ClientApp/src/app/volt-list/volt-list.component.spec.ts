import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoltListComponent } from './volt-list.component';

describe('VoltListComponent', () => {
  let component: VoltListComponent;
  let fixture: ComponentFixture<VoltListComponent>;

  beforeEach(async(() => {
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
