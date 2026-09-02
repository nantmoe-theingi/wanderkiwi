import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DestinationItem, RegionItem } from '../../../models/destination-item.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FavoritesService } from '../../../services/favorites.service';

@Component({
  selector: 'app-destination-results',
  imports: [CommonModule, FormsModule],
  templateUrl: './destination-results.component.html',
  styleUrl: './destination-results.component.scss'
})
export class DestinationResultsComponent implements OnInit {
  @Input() searchResults: DestinationItem[] = [];
  @Input() isSearching: boolean = true;
  @Input() searchQuery: string = '';
  @Input() regions: RegionItem[] = [];
  @Input() showRegionFilter: boolean = false;
  @Input() isFavoritesMode: boolean = false;

  // Filter & Sort States
  selectedRegion: string = '';
  selectedCategory: string = 'All';
  sortBy: string = 'recommended';
  // selectedWeather: string = 'Any';
  selectedBestTime: string = 'Any time';
  selectedActivityLevel: string = 'Any';

  categories: string[] = ['All', 'Nature', 'Adventure', 'Sightseeing', 'Culture', 'Food & Wine'];
  // weatherOptions: string[] = ['Any', 'Sunny', 'Rainy', 'Snowy'];
  bestTimeOptions: string[] = ['Any time', 'Dec - Feb', 'Nov - Mar', 'Sep - Apr', 'Year round'];
  activityLevelOptions: string[] = ['Any', 'Easy', 'Moderate', 'Challenging'];

  // Outputs to notify parent component when filters or sort options change
@Output() filterChange = new EventEmitter<any>();
  @Output() clearAllFilters = new EventEmitter<void>();

  constructor(private favoritesService: FavoritesService) {}
  
  ngOnInit() {
    // Sync initial bookmark state from localStorage when results load
    this.searchResults.forEach(item => {
      item.isBookmarked = this.favoritesService.isBookmarked(item.id);
    });
  }

  onToggleBookmark(item: DestinationItem, event: Event) {
    event.stopPropagation(); // Prevents bubbling if card is clicked
    this.favoritesService.toggleBookmark(item);
  }

  onFilterChange() {
    this.emitFilterState();
  }

  selectCategory(cat: string) {
    this.selectedCategory = cat;
    this.emitFilterState();
  }

  // selectWeather(weather: string) {
  //   this.selectedWeather = weather;
  //   this.emitFilterState();
  // }

  onSortChange() {
    this.emitFilterState();
  }

  clearFilters() {
    this.selectedRegion = '';
    this.selectedCategory = 'All';
    // this.selectedWeather = 'Any';
    this.selectedBestTime = 'Any time';
    this.selectedActivityLevel = 'Any';
    this.sortBy = 'recommended';
    this.clearAllFilters.emit();
  }


  private emitFilterState() {
    this.filterChange.emit({
      region: this.selectedRegion,
      category: this.selectedCategory,
      // weather: this.selectedWeather,
      bestTime: this.selectedBestTime,
      activityLevel: this.selectedActivityLevel,
      sort: this.sortBy
    });
  }
}
