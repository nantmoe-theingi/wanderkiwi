import { Component, OnInit } from '@angular/core';
import { TripService } from '../../services/trip.service';
import { Trip } from '../../models/trip';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-trips',
  imports: [CommonModule],
  templateUrl: './trips.component.html',
  styleUrl: './trips.component.scss'
})
export class TripsComponent implements OnInit {
  trips: Trip[] = [];

  constructor(private tripService: TripService) {}

  ngOnInit(): void {
    this.tripService.getTrips().subscribe({
      next: (data) => this.trips = data,
      error: (err) => console.error('Error loading trips', err)
    });
  }
}
