import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TripPlanRequest } from '../../../models/trip-planner.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-trip-planner-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './trip-planner-form.component.html',
  styleUrl: './trip-planner-form.component.scss',
})
export class TripPlannerFormComponent {
  @Input() isSubmitting = false;
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
    destination: 'Queenstown, New Zealand',
    startDate: '',
    endDate: '',
    travelers: '2 Adults',
    tripStyle: 'Adventure',
    interests: ['Nature', 'Adventure'],
    budgetRange: 'Mid-range',
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
    if (!this.request.destination.trim() || !this.request.startDate || !this.request.endDate) {
      return;
    }
    this.formSubmitted.emit(this.request);
  }
}
