import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeatureManagement } from './feature-management';

describe('FeatureManagement', () => {
  let component: FeatureManagement;
  let fixture: ComponentFixture<FeatureManagement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FeatureManagement],
    }).compileComponents();

    fixture = TestBed.createComponent(FeatureManagement);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
