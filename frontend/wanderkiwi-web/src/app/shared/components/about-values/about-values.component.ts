import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-about-values',
  imports: [CommonModule],
  templateUrl: './about-values.component.html',
  styleUrl: './about-values.component.scss',
})
export class AboutValuesComponent {
  constructor(private router: Router) {}

  values = [
    {
      icon: '⛰️',
      title: 'Authenticity',
      desc: 'We celebrate the real New Zealand.',
    },
    {
      icon: '🌿',
      title: 'Sustainability',
      desc: 'We care for our land, people, and future.',
    },
    {
      icon: '🤝',
      title: 'Local First',
      desc: 'We support local businesses and communities.',
    },
    {
      icon: '🧭',
      title: 'Curiosity',
      desc: 'We encourage exploration and new discoveries.',
    },
    {
      icon: '❤️',
      title: 'Hospitality',
      desc: 'We welcome every traveler like a local friend.',
    },
  ];

  goToPlanner() {
    this.router.navigate(['/trip-planner']);
  }
}
