import { Component, OnInit } from '@angular/core';
import { Attraction } from '../../models/attraction.model';
import { AttractionService } from '../../services/attraction.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  attractions: Attraction[] = [];
  searchTerm: string = '';
  regionFilter: string = '';

  constructor(private attractionService: AttractionService) {}

  ngOnInit(): void {
    this.loadAttractions();
  }

  loadAttractions(): void {
    this.attractionService.getAttractions(this.searchTerm, this.regionFilter).subscribe({
      next: (data) => {
        this.attractions = data;
      },
      error: (err) => {
        console.error('Failed to fetch attractions from API', err);
      }
    });
  }

}
