import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cta-banner',
  imports: [],
  templateUrl: './cta-banner.component.html',
  styleUrl: './cta-banner.component.scss',
})
export class CtaBannerComponent {
  constructor(private router: Router) {}

  goToPlanner() {
    this.router.navigate(['/trip-planner']);
  }
}
