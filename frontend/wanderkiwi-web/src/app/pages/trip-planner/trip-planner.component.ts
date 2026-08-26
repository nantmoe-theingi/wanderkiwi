import { Component } from '@angular/core';
import { TripPlannerService } from '../../services/trip-planner.service';
import { CommonModule } from '@angular/common';
import { TripPlannerFormComponent } from '../../shared/components/trip-planner-form/trip-planner-form.component';
import { TripItineraryComponent } from '../../shared/components/trip-itinerary/trip-itinerary.component';
import { TripPlanResponse } from '../../models/trip-plan-response';
import { TripPlanRequest } from '../../models/trip-plan-request.model';
import { TripResponse } from '../../models/trip-view.model';
import { TripViewComponent } from '../../shared/components/trip-view/trip-view.component';

@Component({
  selector: 'app-trip-planner',
  imports: [CommonModule, TripPlannerFormComponent, TripItineraryComponent, TripViewComponent],
  templateUrl: './trip-planner.component.html',
  styleUrl: './trip-planner.component.scss'
})
export class TripPlannerComponent {
  // tripPlan: TripPlanResponse | null = null;
  // isSubmitting = false;
  // errorMessage = '';

  // constructor(private tripPlannerService: TripPlannerService) {}

  // onGenerateTrip(request: TripPlanRequest) {
  //   this.isSubmitting = true;
  //   this.errorMessage = '';

  //   this.tripPlannerService.createTrip(request).subscribe({
  //     next: (response) => {
  //       this.tripPlan = response;
  //       this.isSubmitting = false;
  //     },
  //     error: (error) => {
  //       this.errorMessage = error.error || 'We could not save your trip. Please try again.';
  //       this.isSubmitting = false;
  //     }
  //   });
  // }

  tripPlan: TripResponse | null = null; // Type it with your new TripResponse interface
  isSubmitting = false;
  errorMessage = '';

  constructor(private tripPlannerService: TripPlannerService) {}

  onGenerateTrip(request: TripPlanRequest) {
  this.isSubmitting = true;
  this.errorMessage = '';

  // The 'request' parameter contains the live user selections
  this.tripPlannerService.createTrip(request).subscribe({
    next: (response: any) => {
      this.tripPlan = response;
      this.isSubmitting = false;
    },
    error: (error) => {
      this.errorMessage = error.error?.message || 'We could not generate your trip. Please try again.';
      this.isSubmitting = false;
    }
  });
}
}
