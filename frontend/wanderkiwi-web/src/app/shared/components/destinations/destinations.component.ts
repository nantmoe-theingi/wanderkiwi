import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-destinations',
  imports: [CommonModule],
  templateUrl: './destinations.component.html',
  styleUrl: './destinations.component.scss'
})
export class DestinationsComponent {
  destinations = [
    { name: 'Queenstown', region: 'Otago', rating: 4.8, description: 'Adventure capital of the world with stunning lakes and mountains.', imageUrl: 'https://images.unsplash.com/photo-1507692049790-de58290a4334?auto=format&fit=crop&w=600&q=80' },
    { name: 'Milford Sound', region: 'Southland', rating: 4.9, description: 'Breathtaking fjord with towering cliffs and waterfalls.', imageUrl: 'https://images.unsplash.com/photo-1469854523086-cc02fe5d8800?auto=format&fit=crop&w=600&q=80' },
    { name: 'Lake Tekapo', region: 'Canterbury', rating: 4.7, description: 'Stunning turquoise lake and world-famous starry skies.', imageUrl: 'https://images.unsplash.com/photo-1518709268805-4e9042af9f23?auto=format&fit=crop&w=600&q=80' },
    { name: 'Rotorua', region: 'Bay of Plenty', rating: 4.6, description: 'Cultural experiences, geothermal wonders and adventure.', imageUrl: 'https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=600&q=80' }
  ];
}
