import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripPlannerHeroComponent } from './trip-planner-hero.component';

describe('TripPlannerHeroComponent', () => {
  let component: TripPlannerHeroComponent;
  let fixture: ComponentFixture<TripPlannerHeroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TripPlannerHeroComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TripPlannerHeroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
