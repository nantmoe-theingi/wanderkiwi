import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { DestinationService } from '../../services/destination.service';
import {
  DestinationItem,
  RegionItem,
} from '../../models/destination-item.model';
import { HeroComponent } from '../../shared/components/hero/hero.component';
import { DestinationResultsComponent } from '../../shared/components/destination-results/destination-results.component';
import { NEW_ZEALAND_ISLANDS } from '../../shared/constants/island.constant';
import { FavoritesService } from '../../services/favorites.service';

@Component({
  selector: 'app-all-destinations',
  imports: [
    CommonModule,
    FormsModule,
    HeroComponent,
    RouterModule,
    DestinationResultsComponent,
  ],
  templateUrl: './all-destinations.component.html',
  styleUrl: './all-destinations.component.scss',
})
export class AllDestinationsComponent implements OnInit {
  searchQuery = '';
  isSearching = false;
  isFavoritesView: boolean = false;
  northIslandImageUrl: string = '/assets/images/north-island.jpg';
  southIslandImageUrl: string = '/assets/images/south-island.jpg';

  // Landing page data structures matching your backend response
  popularDestinations: DestinationItem[] = [];
  regions: RegionItem[] = [];
  featuredAttractions: DestinationItem[] = [];

  selectedWeather: string = 'Any';
  selectedBestTime: string = 'Any time';
  selectedActivityLevel: string = 'Any';
  selectedRegion: string = '';
  selectedCategory: string = 'All';
  sortBy: string = 'recommended';
  // Master copy of search results
  allSearchResults: DestinationItem[] = [];
  // Filtered / Search results view
  searchResults: DestinationItem[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private destinationService: DestinationService,
    private favoritesService: FavoritesService,
  ) {}

  ngOnInit() {
    // Listen to query parameters changing (e.g. searching vs clicking favorites)
    this.route.queryParams.subscribe((params) => {
      if (params['mode'] === 'favorites' || params['search'] === 'favorites') {
        this.isFavoritesView = true;
        this.isSearching = true;
        this.searchQuery = 'Your Favorite Attractions';
        this.loadFavoriteDestinations();
      } else if (params['search']) {
        this.isSearching = true;
        this.isFavoritesView = false;
        this.searchQuery = params['search'];
        this.performSearch(this.searchQuery);
      } else {
        this.isFavoritesView = false;
        this.isSearching = false;
        this.loadLandingData();
      }
    });

    // this.route.queryParams.subscribe((params) => {
    //   this.searchQuery = params['search'] || '';

    //   if (this.searchQuery.trim()) {
    //     this.isSearching = true;
    //     this.performSearch(this.searchQuery);
    //   } else {
    //     this.isSearching = false;
    //     this.loadLandingData();
    //   }
    // });

    // Sync initial bookmark state from localStorage when results load
    this.searchResults.forEach((attraction) => {
      attraction.isBookmarked = this.favoritesService.isBookmarked(
        attraction.id,
      );
    });
  }

  // Load initial landing data (Popular destinations, regions, attractions)
  loadLandingData() {
    this.destinationService.getLandingPageData().subscribe({
      next: (data) => {
        this.popularDestinations = data.popularDestinations;
        this.regions = data.regions;

        // 1. Loop through data and check localStorage for each item's ID
        data.featuredAttractions.forEach((item) => {
          item.isBookmarked = this.favoritesService.isBookmarked(item.id);
        });
        this.featuredAttractions = data.featuredAttractions;
      },
      error: (err) => console.error('Error loading landing page data', err),
    });
  }

  // Calls backend search endpoint
  performSearch(query: string) {
    this.destinationService.searchAttractions(query).subscribe({
      next: (data) => {
        // 1. Loop through data and check localStorage for each item's ID
        data.forEach((item) => {
          item.isBookmarked = this.favoritesService.isBookmarked(item.id);
        });

        this.allSearchResults = data; // Save the original list
        this.searchResults = [...data]; // Initialize display list
      },
      error: (err) => console.error('Error performing search', err),
    });
  }

  onToggleBookmark(attraction: DestinationItem, event: Event) {
    event.stopPropagation(); // Prevents bubbling if card is clicked
    this.favoritesService.toggleBookmark(attraction);
  }

  // Triggered when user searches from Hero component
  onHeroSearch(query: string) {
    if (query.trim()) {
      this.router.navigate(['/all-destinations'], {
        queryParams: { search: query },
      });
    } else {
      this.router.navigate(['/all-destinations']);
    }
  }

  get northIslandRegions() {
    return this.regions.filter((r) => r.islandName === 'North Island');
  }

  get southIslandRegions() {
    return this.regions.filter((r) => r.islandName === 'South Island');
  }

  // Returns only regions that belong to the currently searched island
  get filteredRegions(): RegionItem[] {
    if (this.searchQuery === NEW_ZEALAND_ISLANDS.NORTH) {
      return this.regions.filter((r) => r.islandName === 'North Island');
    } else if (this.searchQuery === NEW_ZEALAND_ISLANDS.SOUTH) {
      return this.regions.filter((r) => r.islandName === 'South Island');
    }
    return this.regions;
  }

  // Triggered when clicking North or South Island buttons
  onSelectIsland(islandName: string) {
    this.router.navigate(['/all-destinations'], {
      queryParams: { search: islandName },
    });
  }

  onParentFilterChange(filters: any) {
    this.selectedRegion = filters.region;
    this.selectedCategory = filters.category;
    this.selectedWeather = filters.weather;
    this.selectedBestTime = filters.bestTime;
    this.selectedActivityLevel = filters.activityLevel;
    this.sortBy = filters.sort;

    let tempResults = [...this.allSearchResults];

    // 1. Region Filter
    if (this.selectedRegion && this.selectedRegion !== 'All Regions') {
      tempResults = tempResults.filter(
        (item) => item.regionName === this.selectedRegion,
      );
    }

    // 2. Category Filter
    if (this.selectedCategory && this.selectedCategory !== 'All') {
      tempResults = tempResults.filter(
        (item) =>
          item.categories && item.categories.includes(this.selectedCategory),
      );
    }

    // 3. Best Time Filter
    if (this.selectedBestTime && this.selectedBestTime !== 'Any time') {
      tempResults = tempResults.filter(
        (item) => item.bestTime === this.selectedBestTime,
      );
    }

    // 4. Activity Level Filter
    if (this.selectedActivityLevel && this.selectedActivityLevel !== 'Any') {
      tempResults = tempResults.filter(
        (item) =>
          item.activityLevel?.toLowerCase() ===
          this.selectedActivityLevel.toLowerCase(),
      );
    }

    // 5. Sorting logic
    if (this.sortBy === 'rating') {
      tempResults.sort((a, b) => b.rating - a.rating);
    } else if (this.sortBy === 'name') {
      tempResults.sort((a, b) => a.name.localeCompare(b.name));
    }

    this.searchResults = tempResults;
  }

  onParentClearFilters() {
    this.selectedRegion = '';
    this.selectedCategory = 'All';
    // this.selectedWeather = 'Any';
    this.selectedBestTime = 'Any time';
    this.selectedActivityLevel = 'Any';
    this.sortBy = 'recommended';

    if (this.isFavoritesView) {
      this.loadFavoriteDestinations(); // Reloads fresh favorites
    } else {
      this.searchResults = [...this.allSearchResults];
    }
  }

  loadFavoriteDestinations() {
    const savedIds = this.favoritesService.getStoredBookmarks();

    // this.destinationService.searchAttractions().subscribe({
    //   next: (destinations) => {
    //     // Filter destinations to only include saved ones
    //     const favoriteItems = destinations.filter(d => {
    //       d.isBookmarked = savedIds.includes(d.id);
    //       return d.isBookmarked;
    //     });

    //     this.allSearchResults = favoriteItems;
    //     this.searchResults = [...favoriteItems];
    //     this.isSearching = true;
    //   },
    //   error: (err) => console.error('Error loading favorites', err)
    // });
  }

  get isIslandSearch(): boolean {
    return (
      this.searchQuery === NEW_ZEALAND_ISLANDS.NORTH ||
      this.searchQuery === NEW_ZEALAND_ISLANDS.SOUTH
    );
  }
}
