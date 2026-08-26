import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TripDayItinerary } from '../../../models/trip-day-itinerary.model';
import { TripPlanResponse } from '../../../models/trip-plan-response';

@Component({
  selector: 'app-trip-itinerary',
  imports: [CommonModule],
  templateUrl: './trip-itinerary.component.html',
  styleUrl: './trip-itinerary.component.scss',
})
export class TripItineraryComponent {
  @Input() tripPlan: TripPlanResponse | null = null;
  activeDayIndex = 0;

  get currentDay(): TripDayItinerary | null {
    if (!this.tripPlan || !this.tripPlan.days.length) return null;
    return this.tripPlan.days[this.activeDayIndex];
  }

  selectDay(index: number) {
    this.activeDayIndex = index;
  }
}
