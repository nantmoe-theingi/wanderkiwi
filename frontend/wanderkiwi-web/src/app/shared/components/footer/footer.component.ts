import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NavItem } from '../../../models/navigation.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-footer',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent {
  email = '';
  logoPath = 'assets/images/wanderkiwi-logo.png';

  // Reusing the NavItem model arrays for dynamic iteration
  quickLinks: NavItem[] = [
    { label: 'Home', route: '/' },
    { label: 'Destinations', route: '/destinations' },
    { label: 'Trip Planner', route: '/trip-planner' },
    { label: 'Articles', route: '/articles' },
    { label: 'About Us', route: '/about' }
  ];

  informationLinks: NavItem[] = [
    { label: 'About WanderKiwi', route: '/about' },
    { label: 'How It Works', route: '/how-it-works' },
    { label: 'Privacy Policy', route: '/privacy' },
    { label: 'Terms of Service', route: '/terms' },
    { label: 'Contact Us', route: '/contact' }
  ];

  supportLinks: NavItem[] = [
    { label: 'Help Center', route: '/help' },
    { label: 'FAQs', route: '/faqs' },
    { label: 'Travel Tips', route: '/tips' },
    { label: 'Community', route: '/community' }
  ];

  subscribe() {
    if (this.email) {
      console.log('Subscribed email:', this.email);
      // can hook up a newsletter service later!
      this.email = '';
    }
  }
}
