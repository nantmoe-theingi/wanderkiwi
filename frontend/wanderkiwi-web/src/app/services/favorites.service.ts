import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DestinationItem } from '../models/destination-item.model';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {
  private favoritesKey = 'wanderkiwi_favorites';
  
  // Optional: tracking count reactively for your header badge
  private favoritesCountSubject = new BehaviorSubject<number>(this.getFavorites().length);
  favoritesCount$ = this.favoritesCountSubject.asObservable();

  constructor() {}

  // Retrieves the list of bookmarked destination items
  getFavorites(): DestinationItem[] {
    const data = localStorage.getItem(this.favoritesKey);
    if (data) {
      try {
        return JSON.parse(data);
      } catch (e) {
        console.error('Error parsing favorites from localStorage', e);
        return [];
      }
    }
    return [];
  }

  // Helper to toggle and update storage/badges
  toggleBookmark(item: DestinationItem) {
    const favorites = this.getFavorites();
    const index = favorites.findIndex(fav => fav.id === item.id);

    if (index > -1) {
      favorites.splice(index, 1);
      item.isBookmarked = false;
    } else {
      item.isBookmarked = true;
      favorites.push(item);
    }

    localStorage.setItem(this.favoritesKey, JSON.stringify(favorites));
    this.favoritesCountSubject.next(favorites.length);
  }

  isBookmarked(id: number): boolean {
    const favorites = this.getFavorites();
    return favorites.some(fav => fav.id === id);
  }
}