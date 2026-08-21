import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DestinationItem } from '../../models/destination-item.model';
import { HeroComponent } from '../../shared/components/hero/hero.component';
import { DestinationService } from '../../services/destination.service';

@Component({
  selector: 'app-all-destinations',
  imports: [CommonModule, FormsModule, HeroComponent],
  templateUrl: './all-destinations.component.html',
  styleUrl: './all-destinations.component.scss',
})
export class AllDestinationsComponent implements OnInit {
  // destinations: DestinationItem[] = [
  //   {
  //     id: 1,
  //     name: 'Queenstown',
  //     region: 'Otago',
  //     rating: 4.8,
  //     reviewsCount: '1.2k reviews',
  //     description:
  //       'Adventure capital of the world. Bungy jumping, skiing, and breathtaking landscapes.',
  //     imageUrl: 'assets/images/queenstown.png',
  //     categories: ['Adventure', 'Nature', 'Sightseeing'],
  //     isPopular: true,
  //   },
  //   {
  //     id: 2,
  //     name: 'Milford Sound',
  //     region: 'Southland',
  //     rating: 4.9,
  //     reviewsCount: '980 reviews',
  //     description:
  //       'Stunning fjord with waterfalls, rainforest, and incredible boat cruises.',
  //     imageUrl: 'assets/images/milford.png',
  //     categories: ['Nature', 'Sightseeing'],
  //     isPopular: true,
  //   },
  //   {
  //     id: 3,
  //     name: 'Rotorua',
  //     region: 'Bay of Plenty',
  //     rating: 4.6,
  //     reviewsCount: '760 reviews',
  //     description:
  //       'Geothermal wonders, Maori culture, and relaxing hot springs.',
  //     imageUrl: 'assets/images/rotorua.jpg',
  //     categories: ['Culture', 'Nature', 'Adventure'],
  //   },
  //   {
  //     id: 4,
  //     name: 'Hobbiton™ Movie Set',
  //     region: 'Waikato',
  //     rating: 4.7,
  //     reviewsCount: '850 reviews',
  //     description:
  //       'Step into the world of Middle-earth. A must-visit for LOTR fans.',
  //     imageUrl: 'assets/images/hobbiton.jpg',
  //     categories: ['Culture', 'Sightseeing'],
  //   },
  // ];

  searchQuery = '';
  selectedRegion = '';
  selectedCategory = 'All';
  sortBy = 'recommended';

  categories = [
    'All',
    'Nature',
    'Adventure',
    'Sightseeing',
    'Culture',
    'Food & Wine',
  ];
  destinations: DestinationItem[] = [];

  constructor(
    private route: ActivatedRoute,
    private destinationService: DestinationService,
  ) {}

  ngOnInit() {
    // Listen to query parameters so if someone searches from the hero, it updates automatically
    this.route.queryParams.subscribe((params) => {
      this.searchQuery = params['search'] || '';
      // Load destinations right away on init (and whenever params change)
      this.loadDestinations();
    });
  }

  // Calls your .NET Core backend API with the search term, region, and category
  loadDestinations() {
    this.destinationService
      .getFilteredDestinations(
        this.searchQuery,
        this.selectedRegion,
        this.selectedCategory,
      )
      .subscribe({
        next: (data) => {
          this.destinations = data;
          this.sortDestinations(); // Apply sorting after fetching
        },
        error: (err) => {
          console.error('Failed to load destinations from backend', err);
        },
      });
  }

  // Triggered when users change the sidebar dropdown filters or categories
  onFilterChange() {
    this.loadDestinations();
  }

  // Triggered when clicking a category pill
  selectCategory(cat: string) {
    this.selectedCategory = cat;
    this.loadDestinations();
  }

  // Triggered when sort dropdown changes
  onSortChange() {
    this.sortDestinations();
  }

  // Sorts the current destinations array locally
  sortDestinations() {
    if (this.sortBy === 'rating') {
      this.destinations.sort((a, b) => b.rating - a.rating);
    } else if (this.sortBy === 'name') {
      this.destinations.sort((a, b) => a.name.localeCompare(b.name));
    } else {
      // Default recommended sorting (e.g., by ID or default order)
      this.destinations.sort((a, b) => a.id - b.id);
    }
  }

  clearFilters() {
    this.selectedRegion = '';
    this.selectedCategory = 'All';
    this.searchQuery = '';
    this.sortBy = 'recommended';
    this.loadDestinations(); // Reload default popular/all items after clearing
  }

  // Changed from a getter to a simple property/method to stop infinite HTTP loops
  get filteredDestinations() {
    return this.destinations;
  }

  // Catch the emitted search text from the Hero component
  onHeroSearch(query: string) {
    this.searchQuery = query;
    this.loadDestinations();
  }
}
