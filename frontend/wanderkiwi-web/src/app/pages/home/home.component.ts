import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroComponent } from '../../shared/components/hero/hero.component';
import { CategoriesComponent } from '../../shared/components/categories/categories.component';
import { WhyUsComponent } from '../../shared/components/why-us/why-us.component';
// import { ItinerariesComponent } from '../../shared/components/itineraries/itineraries.component';
import { DestinationsComponent } from '../../shared/components/destinations/destinations.component';

@Component({
  selector: 'app-home',
  imports: [CommonModule,
    HeroComponent,
    DestinationsComponent,
    CategoriesComponent,
    // ItinerariesComponent,
    WhyUsComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  
}
