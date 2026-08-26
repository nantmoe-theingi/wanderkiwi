import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TripDay, TripResponse } from '../../../models/trip-view.model';

@Component({
  selector: 'app-trip-view',
  imports: [CommonModule],
  templateUrl: './trip-view.component.html',
  styleUrl: './trip-view.component.scss'
})
export class TripViewComponent {

  // Tracks which day tab is currently open (defaults to Day 1 / index 0)
  activeDayIndex: number = 0;

  @Input() trip: TripResponse | null = null;

  get currentDay(): TripDay | null {
    if (!this.trip || !this.trip.days || this.trip.days.length === 0) {
      return null;
    }
    return this.trip.days[this.activeDayIndex] || this.trip.days[0];
  }

  selectDay(index: number) {
    this.activeDayIndex = index;
  }

}
