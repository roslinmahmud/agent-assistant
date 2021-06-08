import { TestBed } from '@angular/core/testing';

import { PasswordConfirmationValidatorsService } from './password-confirmation-validators.service';

describe('PasswordConfirmationValidatorsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PasswordConfirmationValidatorsService = TestBed.get(PasswordConfirmationValidatorsService);
    expect(service).toBeTruthy();
  });
});
