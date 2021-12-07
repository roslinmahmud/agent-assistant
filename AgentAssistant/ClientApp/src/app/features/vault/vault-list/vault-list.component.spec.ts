import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VaultListComponent } from './vault-list.component';

describe('VaultListComponent', () => {
  let component: VaultListComponent;
  let fixture: ComponentFixture<VaultListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ VaultListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VaultListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
