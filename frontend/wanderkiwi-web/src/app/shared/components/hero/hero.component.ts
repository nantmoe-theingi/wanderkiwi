import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchService } from '../../../services/search.service';
import { SearchFilter } from '../../../models/search-filter.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DestinationService } from '../../../services/destination.service';

@Component({
  selector: 'app-hero',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss'
})
export class HeroComponent implements OnInit {
  @Input() title: string = 'Discover New Zealand';
  @Input() subtitle: string = 'Your next adventure starts here. AI-powered trip planning made easy.';
  @Input() placeholderText: string = 'Search destinations, places, activities...';
  @Input() showPopularTags: boolean = true;
  @Input() searchQuery: string = '';
  destinations: any[] = [];

  @Output() searchSubmitted = new EventEmitter<string>();

  popularTags = ['Queenstown', 'Milford Sound', 'Hobbiton', 'Rotorua', 'Wanaka'];
  
  constructor(private searchService: SearchService, private route: ActivatedRoute,
    private router: Router, private destinationService: DestinationService) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['search'] || '';
      this.fetchFilteredDestinations(this.searchQuery);
    });
  }

  onSearch() {
    const trimmedQuery = this.searchQuery ? this.searchQuery.trim() : '';
    
    // 1. Update the route with query parameters so the URL matches
    this.router.navigate(['/all-destinations'], { queryParams: { search: trimmedQuery } });
    
    // 2. Emit the search to the parent AllDestinationsComponent
    this.searchSubmitted.emit(trimmedQuery);
    
    const filter: SearchFilter = { keyword: trimmedQuery };
    this.searchService.updateSearch(filter);
  }

  private fetchFilteredDestinations(keyword: string) {
    this.destinationService.searchAttractions(keyword).subscribe({
      next: (data) => {
        this.destinations = data;
        console.log('Fetched destinations from backend:', this.destinations);
      },
      error: (err) => {
        console.error('Error fetching search results from backend', err);
      }
    });
  }

  selectTag(tag: string) {
    this.searchQuery = tag;
    const filter: SearchFilter = { keyword: tag };
    this.searchService.updateSearch(filter);
    
    if (this.router.url.includes('/all-destinations')) {
      this.fetchFilteredDestinations(tag);
    } else {
      this.router.navigate(['/all-destinations'], { queryParams: { search: tag } });
    }
    
    this.searchSubmitted.emit(tag);
  }
}
