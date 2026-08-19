import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { HeroComponent } from '../hero/hero.component';

@Component({
  selector: 'app-trip-planner-hero',
  imports: [CommonModule, HeroComponent],
  templateUrl: './trip-planner-hero.component.html',
  styleUrl: './trip-planner-hero.component.scss'
})
export class TripPlannerHeroComponent {

}
