import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TripPlanRequest } from '../../../models/trip-plan-request.model';
import { DestinationLookup } from '../../../models/destination-lookup.model';
import { DestinationService } from '../../../services/destination.service';
import { DestinationItem } from '../../../models/destination-item.model';

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
  destinationNames: DestinationLookup[] = [];
  filteredDestinations: DestinationLookup[] = [];
  isDropdownOpen: boolean = false;
  selectedDestination: DestinationLookup | null = null;
  errorMessage: string = '';
  todayDateString: string = '';
  maxEndDateString: string = '';


  @Output() formSubmitted = new EventEmitter<TripPlanRequest>();

  request: TripPlanRequest = {
    destination: 'Queenstown',
    startDate: new Date().toISOString().split('T')[0], // Default to today's date in YYYY-MM-DD format
    endDate: new Date().toISOString().split('T')[0],
    startTime: '08:00', // Default start time set to 8:00 AM
    travellers: 2,
    tripStyle: 'Adventure',
    interests: ['Nature', 'Adventure'],
    budget: 'Mid-range',
    transportMode: 'Car',
  };
    
  constructor(private destinationService: DestinationService) {}
  
    ngOnInit() {
      // Get today's date formatted as YYYY-MM-DD (e.g. "2026-09-02")
      const today = new Date();
      this.todayDateString = this.formatDate(today);
      
      // 1. Set BOTH start date and end date to today initially
      this.request.startDate = this.todayDateString;
      this.request.endDate = this.todayDateString;

      // 2. Set the max boundary limit based on the start date
      this.updateMaxEndDate(this.request.startDate);

      this.destinationService.getDestinationNames().subscribe({
        next: (data) => {
          this.destinationNames = data as any[]; 
        },
        error: (err) => console.error('Error loading destination names', err)
      });
  }

  // Triggered when user changes the start date
  onStartDateChange() {
    if (this.request.startDate) {
      this.updateMaxEndDate(this.request.startDate);

      // If the current end date is older than the new start date, reset end date to match the start date
      if (this.request.endDate < this.request.startDate || this.request.endDate > this.maxEndDateString) {
        this.request.endDate = this.request.startDate;
      }
    }
  }

  // Calculates the maximum allowed end date range (e.g., 7 days limit)
  updateMaxEndDate(start: string) {
    const startDt = new Date(start);
    startDt.setDate(startDt.getDate() + 6); // Max 1 week limit
    this.maxEndDateString = this.formatDate(startDt);
  }

  // Helper to format Date object to YYYY-MM-DD string
  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

    // Triggered as the user types in the text box
  onInputChange() {
    const query = this.request.destination.trim().toLowerCase();
    
    if (query.length > 0) {
      // Filter destinations starting with or containing the typed string
      this.filteredDestinations = this.destinationNames.filter(d => 
        d.name.toLowerCase().includes(query)
      );
      this.isDropdownOpen = this.filteredDestinations.length > 0;
    } else {
      this.filteredDestinations = [];
      this.isDropdownOpen = false;
    }
    
    this.selectedDestination = null; // Reset selection until clicked/validated
    this.errorMessage = '';
  }

  // Triggered when a user clicks an option from the dropdown
  selectDestination(dest: DestinationLookup) {
    this.request.destination = dest.name;
    this.selectedDestination = dest;
    this.isDropdownOpen = false;
    this.errorMessage = '';
  }

  // Validation before submitting or moving forward
  validateAndPlanTrip() {
    // Check if the typed destination exists in your loaded DB destinations list
    const match = this.destinationNames.find(
      d => d.name.toLowerCase() === this.request.destination.trim().toLowerCase()
    );

    if (!match) {
      this.errorMessage = 'Please select a valid New Zealand destination from the list.';
      return;
    }

    this.selectedDestination = match;
    this.errorMessage = '';
    
    // Proceed with your trip planning logic using this.selectedDestination
    console.log('Trip planned for:', this.selectedDestination.name);
  }


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

    // Check if the typed destination exists in your loaded DB destinations list
    const match = this.destinationNames.find(
      d => d.name.toLowerCase() === this.request.destination.trim().toLowerCase()
    );

    if (!match) {
      this.errorMessage = 'Please select a valid New Zealand destination from the list.';
      return;
    }

    this.selectedDestination = match;
    this.errorMessage = '';

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
      startTime: timeToSend,
    };

    // All validations passed, emit the request
    this.formSubmitted.emit(payload);
  }
}

  // Helper function for date string max limit
function maxDateString(start: string): string {
  const d = new Date(start);
  d.setDate(d.getDate() + 7);
  const year = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}
