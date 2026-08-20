import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-about-why-features',
  imports: [CommonModule],
  templateUrl: './about-why-features.component.html',
  styleUrl: './about-why-features.component.scss',
})
export class AboutWhyFeaturesComponent {
  features = [
    {
      icon: '✨',
      title: 'AI-Powered Planning',
      desc: 'Smart recommendations tailored to your preferences, interests, and travel style.',
    },
    {
      icon: '📍',
      title: 'Discover Hidden Gems',
      desc: 'Find unique places and local secrets that most travelers miss.',
    },
    {
      icon: '🗺️',
      title: 'Personalised Itineraries',
      desc: 'Custom day-by-day plans that save you time and maximise your adventure.',
    },
    {
      icon: '⛅',
      title: 'Real-Time Updates',
      desc: 'Get the latest weather, conditions, and travel tips at your fingertips.',
    },
    {
      icon: '🥝',
      title: '100% New Zealand',
      desc: 'Built with love for Aotearoa, supporting local experiences and communities.',
    },
  ];
}
