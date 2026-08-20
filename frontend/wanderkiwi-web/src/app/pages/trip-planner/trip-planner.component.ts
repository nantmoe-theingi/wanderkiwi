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

  constructor(private tripPlannerService: TripPlannerService) {}

  onGenerateTrip(request: TripPlanRequest) {
    this.tripPlannerService.generateTrip(request).subscribe(response => {
      this.tripPlan = response;
    });
  }
}
