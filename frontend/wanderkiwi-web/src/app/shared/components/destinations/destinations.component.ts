import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AttractionService } from '../../../services/attraction.service';
import { Attraction } from '../../../models/attraction.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-destinations',
  imports: [CommonModule, RouterModule],
  templateUrl: './destinations.component.html',
  styleUrl: './destinations.component.scss'
})
export class DestinationsComponent implements OnInit {
  destinations: Attraction[] = [];

  constructor(private attractionService: AttractionService) {}

  ngOnInit(): void {
    this.loadDestinations();
  }

  loadDestinations() {
    this.attractionService.getDestinations().subscribe({
      next: (data) => {
        this.destinations = data;
      },
      error: (err) => {
        console.error('Error loading attractions', err);
      }
    });
  }
}
