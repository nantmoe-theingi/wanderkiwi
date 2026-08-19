import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchService } from '../../../services/search.service';
import { SearchFilter } from '../../../models/search-filter.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-hero',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss'
})
export class HeroComponent {
  @Input() title: string = 'Discover New Zealand';
  @Input() subtitle: string = 'Your next adventure starts here. AI-powered trip planning made easy.';
  @Input() placeholderText: string = 'Search destinations, places, activities...';
  @Input() showPopularTags: boolean = true;
  @Input() searchQuery: string = '';

  @Output() searchSubmitted = new EventEmitter<string>();

  popularTags = ['Queenstown', 'Milford Sound', 'Hobbiton', 'Rotorua', 'Wanaka'];
  
  constructor(private searchService: SearchService, private router: Router) {}

  onSearch() {
    if (this.searchQuery.trim()) {
      this.router.navigate(['/destinations'], { queryParams: { search: this.searchQuery } });
    } else {
      this.router.navigate(['/destinations']);
    }
    
    const filter: SearchFilter = { keyword: this.searchQuery };
    this.searchService.updateSearch(filter);
    this.searchSubmitted.emit(this.searchQuery);
  }

  selectTag(tag: string) {
    this.searchQuery = tag;
    const filter: SearchFilter = { keyword: tag };
    this.searchService.updateSearch(filter);
    
    this.router.navigate(['/destinations'], { queryParams: { search: tag } });
    this.searchSubmitted.emit(tag);
  }
}
