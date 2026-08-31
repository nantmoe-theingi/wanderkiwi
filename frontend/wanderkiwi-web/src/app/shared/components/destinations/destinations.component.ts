import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DestinationService } from '../../../services/destination.service';
import { DestinationLookup } from '../../../models/destination-lookup.model';

@Component({
  selector: 'app-destinations',
  imports: [CommonModule, RouterModule],
  templateUrl: './destinations.component.html',
  styleUrl: './destinations.component.scss'
})
export class DestinationsComponent implements OnInit {
  destinations: DestinationLookup[] = [];

  constructor(private desService: DestinationService) {}

  ngOnInit(): void {
    this.loadDestinations();
  }

  loadDestinations() {
    this.desService.getPopularDestinations().subscribe({
      next: (data) => {
        this.destinations = data;
      },
      error: (err) => {
        console.error('Error loading attractions', err);
      }
    });
  }
}
