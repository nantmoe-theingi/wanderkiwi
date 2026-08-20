import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AboutStoryMissionComponent } from '../../shared/components/about-story-mission/about-story-mission.component';
import { AboutWhyFeaturesComponent } from '../../shared/components/about-why-features/about-why-features.component';
import { AboutValuesComponent } from '../../shared/components/about-values/about-values.component';
import { AboutHeroComponent } from '../../shared/components/about-hero/about-hero.component';

@Component({
  selector: 'app-about-us',
  imports: [CommonModule, 
    AboutHeroComponent, 
    AboutStoryMissionComponent, 
    AboutWhyFeaturesComponent, 
    AboutValuesComponent],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.scss'
})
export class AboutUsComponent {

}
