import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TripPlanRequest } from '../../../models/trip-plan-request.model';

@Component({
  selector: 'app-trip-planner-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './trip-planner-form.component.html',
  styleUrl: './trip-planner-form.component.scss',
})
export class TripPlannerFormComponent {
  @Input() isSubmitting = false;
  formError: string = '';
  availableInterests = [
    'Nature',
    'Adventure',
    'Culture',
    'Food & Wine',
    'Relaxation',
    'Wildlife',
  ];

  @Output() formSubmitted = new EventEmitter<TripPlanRequest>();

 request: TripPlanRequest = {
    destination: 'Queenstown',
    startDate: new Date().toISOString().split('T')[0],  // Default to today's date in YYYY-MM-DD format
    endDate: new Date().toISOString().split('T')[0],
    startTime: '08:00', // Default start time set to 8:00 AM
    travellers: 2,
    tripStyle: 'Adventure',
    interests: ['Nature', 'Adventure'],
    budget: 'Mid-range',
    transportMode: 'Car'
  };

  isInterestSelected(interest: string): boolean {
    return this.request.interests.includes(interest);
  }

  toggleInterest(interest: string) {
    const index = this.request.interests.indexOf(interest);
    if (index > -1) {
      this.request.interests.splice(index, 1);
    } else {
      if (this.request.interests.length < 3) {
        this.request.interests.push(interest);
      }
    }
  }

 onSubmit() {
    this.formError = ''; // Reset error message on new click

    // 1. Check required text fields
    if (!this.request.destination || !this.request.destination.trim()) {
      this.formError = 'Please enter a destination.';
      return;
    }

    // 2. Check required dates
    if (!this.request.startDate || !this.request.endDate) {
      this.formError = 'Please select both a start and end date.';
      return;
    }

    // 3. Validate Date Logic (Start date cannot be after end date)
    const start = new Date(this.request.startDate);
    const end = new Date(this.request.endDate);

    if (start > end) {
      this.formError = 'The start date cannot be later than the end date.';
      return;
    }

    // 4. Validate Travelers count
    this.request.travellers = Number(this.request.travellers);
    if (!this.request.travellers || this.request.travellers < 1) {
      this.formError = 'Please specify at least 1 traveler.';
      return;
    }

    // 5. Validate Interests selection
    if (!this.request.interests || this.request.interests.length === 0) {
      this.formError = 'Please select at least one interest.';
      return;
    }

    // Fallback if start time is empty, then format to HH:mm:ss
    let timeToSend = this.request.startTime || '08:00';
    if (timeToSend.length === 5) {
      timeToSend = `${timeToSend}:00`;
    }

    // Create a clean payload object with the formatted time for the backend
    const payload: TripPlanRequest = {
      ...this.request,
      startTime: timeToSend
    };

    // All validations passed, emit the request
    this.formSubmitted.emit(payload);
  }
}
