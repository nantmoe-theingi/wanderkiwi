import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Itinerary } from '../../../models/Itinerary.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-itineraries',
  imports: [CommonModule, RouterModule],
  templateUrl: './itineraries.component.html',
  styleUrl: './itineraries.component.scss',
})
export class ItinerariesComponent {
  itineraries: Itinerary[] = [
    {
      title: 'Weekend in Queenstown',
      duration: '3 Days',
      tag: 'Adventure',
      imageUrl:
        'https://images.unsplash.com/photo-1507692049790-de58290a4334?auto=format&fit=crop&w=600&q=80',
    },
    {
      title: 'South Island Explorer',
      duration: '7 Days',
      tag: 'Road Trip',
      imageUrl:
        'https://images.unsplash.com/photo-1469854523086-cc02fe5d8800?auto=format&fit=crop&w=600&q=80',
    },
    {
      title: 'North Island Highlights',
      duration: '5 Days',
      tag: 'Family',
      imageUrl:
        'https://images.unsplash.com/photo-1518709268805-4e9042af9f23?auto=format&fit=crop&w=600&q=80',
    },
  ];
}
