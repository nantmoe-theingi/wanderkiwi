import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-why-us',
  imports: [CommonModule],
  templateUrl: './why-us.component.html',
  styleUrl: './why-us.component.scss'
})
export class WhyUsComponent {
features = [
    { icon: '🤖', title: 'AI Trip Planner', desc: 'Get personalised itinerary recommendations powered by AI.' },
    { icon: '🗺️', title: 'Interactive Maps', desc: 'Explore places with our interactive maps and local insights.' },
    { icon: '⛅', title: 'Weather Forecast', desc: 'Check real-time weather forecasts for your travel dates.' },
    { icon: '💰', title: 'Budget Estimator', desc: 'Plan your trip with our smart budget planning tools.' },
    { icon: '❤️', title: 'Save Favourites', desc: 'Save your favourite places and access them anytime.' }
  ];
}
