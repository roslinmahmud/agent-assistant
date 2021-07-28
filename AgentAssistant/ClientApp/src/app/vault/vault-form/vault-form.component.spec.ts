import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VaultFormComponent } from './vault-form.component';

describe('VaultFormComponent', () => {
  let component: VaultFormComponent;
  let fixture: ComponentFixture<VaultFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ VaultFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
