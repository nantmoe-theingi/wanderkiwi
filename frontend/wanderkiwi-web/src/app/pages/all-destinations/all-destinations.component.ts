import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DestinationItem } from '../../models/destination-item.model';
import { HeroComponent } from '../../shared/components/hero/hero.component';

@Component({
  selector: 'app-all-destinations',
  imports: [CommonModule, FormsModule, HeroComponent],
  templateUrl: './all-destinations.component.html',
  styleUrl: './all-destinations.component.scss'
})
export class AllDestinationsComponent implements OnInit {
  searchQuery = '';
  selectedRegion = '';
  selectedCategory = 'All';
  sortBy = 'recommended';

  categories = ['All', 'Nature', 'Adventure', 'Sightseeing', 'Culture', 'Food & Wine'];

  destinations: DestinationItem[] = [
    { id: 1, name: 'Queenstown', region: 'Otago', rating: 4.8, reviewsCount: '1.2k reviews', description: 'Adventure capital of the world. Bungy jumping, skiing, and breathtaking landscapes.', imageUrl: 'assets/images/queenstown.png', categories: ['Adventure', 'Nature', 'Sightseeing'], isPopular: true },
    { id: 2, name: 'Milford Sound', region: 'Southland', rating: 4.9, reviewsCount: '980 reviews', description: 'Stunning fjord with waterfalls, rainforest, and incredible boat cruises.', imageUrl: 'assets/images/milford.png', categories: ['Nature', 'Sightseeing'], isPopular: true },
    { id: 3, name: 'Rotorua', region: 'Bay of Plenty', rating: 4.6, reviewsCount: '760 reviews', description: 'Geothermal wonders, Maori culture, and relaxing hot springs.', imageUrl: 'assets/images/rotorua.jpg', categories: ['Culture', 'Nature', 'Adventure'] },
    { id: 4, name: 'Hobbiton™ Movie Set', region: 'Waikato', rating: 4.7, reviewsCount: '850 reviews', description: 'Step into the world of Middle-earth. A must-visit for LOTR fans.', imageUrl: 'assets/images/hobbiton.jpg', categories: ['Culture', 'Sightseeing'] }
  ];

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['search']) {
        this.searchQuery = params['search'];
      }
    });
  }

  clearFilters() {
    this.selectedRegion = '';
    this.selectedCategory = 'All';
    this.searchQuery = '';
  }

  get filteredDestinations() {
    return this.destinations.filter(item => {
      const matchesSearch = !this.searchQuery || 
        item.name.toLowerCase().includes(this.searchQuery.toLowerCase()) || 
        item.region.toLowerCase().includes(this.searchQuery.toLowerCase());
      
      const matchesRegion = !this.selectedRegion || item.region === this.selectedRegion;
      const matchesCategory = this.selectedCategory === 'All' || item.categories.includes(this.selectedCategory);

      return matchesSearch && matchesRegion && matchesCategory;
    });
  }
}
