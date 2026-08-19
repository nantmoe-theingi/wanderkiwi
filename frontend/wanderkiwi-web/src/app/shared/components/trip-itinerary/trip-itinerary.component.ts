import { Component, Input } from '@angular/core';
import { TripPlanResponse } from '../../../models/trip-planner.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-trip-itinerary',
  imports: [CommonModule],
  templateUrl: './trip-itinerary.component.html',
  styleUrl: './trip-itinerary.component.scss',
})
export class TripItineraryComponent {
  @Input() tripPlan: TripPlanResponse | null = null;
}
