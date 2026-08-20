import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutWhyFeaturesComponent } from './about-why-features.component';

describe('AboutWhyFeaturesComponent', () => {
  let component: AboutWhyFeaturesComponent;
  let fixture: ComponentFixture<AboutWhyFeaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AboutWhyFeaturesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutWhyFeaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
