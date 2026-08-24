import { Component } from '@angular/core';
import { TripPlanRequest, TripPlanResponse } from '../../models/trip-planner.model';
import { TripPlannerService } from '../../services/trip-planner.service';
import { CommonModule } from '@angular/common';
import { TripPlannerFormComponent } from '../../shared/components/trip-planner-form/trip-planner-form.component';
import { TripItineraryComponent } from '../../shared/components/trip-itinerary/trip-itinerary.component';

@Component({
  selector: 'app-trip-planner',
  imports: [CommonModule, TripPlannerFormComponent, TripItineraryComponent],
  templateUrl: './trip-planner.component.html',
  styleUrl: './trip-planner.component.scss'
})
export class TripPlannerComponent {
  tripPlan: TripPlanResponse | null = null;
  isSubmitting = false;
  errorMessage = '';

  constructor(private tripPlannerService: TripPlannerService) {}

  onGenerateTrip(request: TripPlanRequest) {
    this.isSubmitting = true;
    this.errorMessage = '';

    this.tripPlannerService.createTrip(request).subscribe({
      next: (response) => {
        this.tripPlan = response;
        this.isSubmitting = false;
      },
      error: (error) => {
        this.errorMessage = error.error || 'We could not save your trip. Please try again.';
        this.isSubmitting = false;
      }
    });
  }
}
