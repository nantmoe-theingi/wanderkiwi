import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Observable } from 'rxjs';
import { WishlistService } from '../../../core/services/wishlist.service';
import { NavItem } from '../../../models/navigation.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [RouterLink, CommonModule, RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  // Use the NavItem model for strict typing
  navItems: NavItem[] = [
    { label: 'Home', route: '/', exactMatch: true },
    { label: 'Destinations', route: '/all-destinations' },
    { label: 'Trip Planner', route: '/trip-planner' },
    { label: 'Articles', route: '/articles' },
    { label: 'About Us', route: '/about-us' }
  ];

  // Observable for tracking the wishlist count reactively
  wishlistCount$!: Observable<number>;

  constructor(private wishlistService: WishlistService, private router: Router) {}

  ngOnInit(): void {
    // Connect to the wishlist service stream
    this.wishlistCount$ = this.wishlistService.favoritesCount$;
  }

  goToPlanner() {
    this.router.navigate(['/trip-planner']);
  }

  onClickBookmark() {
    // Navigate to the wishlist page when the bookmark button is clicked  
  this.router.navigate(['/all-destinations'], { queryParams: { mode: 'favorites' } });
  }
}