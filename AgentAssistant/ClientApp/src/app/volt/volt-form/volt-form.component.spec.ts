import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoltFormComponent } from './volt-form.component';

describe('VoltFormComponent', () => {
  let component: VoltFormComponent;
  let fixture: ComponentFixture<VoltFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoltFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoltFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
