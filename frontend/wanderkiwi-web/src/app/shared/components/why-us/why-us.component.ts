import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-why-us',
  imports: [CommonModule],
  templateUrl: './why-us.component.html',
  styleUrl: './why-us.component.scss',
})
export class WhyUsComponent implements OnInit, OnDestroy, AfterViewInit {
  features = [
    { icon: '🤖', title: 'AI Trip Planner', desc: 'Get personalised itinerary recommendations powered by AI.', isComingSoon: false },
    { icon: '🔍', title: 'Smart Island & Region Filters', desc: 'Easily filter New Zealand attractions by island, category, and activity level.', isComingSoon: false },
    { icon: '📚', title: 'Travel Articles & Guides', desc: 'Read inspiration, expert tips, and local insights to explore New Zealand like a local.', isComingSoon: false },
    { icon: '❤️', title: 'Save Favourites', desc: 'Save your favorite places locally and access them anytime.', isComingSoon: false },
    { icon: '🗺️', title: 'Interactive Maps', desc: 'Explore destinations dynamically with geospatial map integration.', isComingSoon: true },
    { icon: '⛅', title: 'Weather Forecast', desc: 'Check live weather conditions and forecasts for your travel dates.', isComingSoon: true },
  ];

  currentIndex: number = 0;
  slideDistance: number = 295; // Approximate width of card (275px + 20px gap)
  private slideInterval: any;

  @ViewChild('viewport', { read: ElementRef }) viewport!: ElementRef;

  ngOnInit() {
    this.startAutoSlide();
  }

  ngAfterViewInit() {
    // Calculate precise slide distance based on actual rendered card size
    setTimeout(() => {
      const cardElement = document.querySelector('.feature-card') as HTMLElement;
      if (cardElement) {
        const cardWidth = cardElement.offsetWidth;
        const gap = 20; // Matches SCSS gap
        this.slideDistance = cardWidth + gap;
      }
    }, 100);
  }

  ngOnDestroy() {
    this.stopAutoSlide();
  }

  startAutoSlide() {
    this.slideInterval = setInterval(() => {
      this.nextSlide();
    }, 4000);
  }

  stopAutoSlide() {
    if (this.slideInterval) clearInterval(this.slideInterval);
  }

  nextSlide() {
    // Max index is total items minus the 4 visible cards (6 - 4 = 2)
    const maxIndex = this.features.length - 4;
    if (this.currentIndex < maxIndex) {
      this.currentIndex++;
    } else {
      this.currentIndex = 0;
    }
  }

  prevSlide() {
    const maxIndex = this.features.length - 4;
    if (this.currentIndex > 0) {
      this.currentIndex--;
    } else {
      this.currentIndex = maxIndex;
    }
  }
}