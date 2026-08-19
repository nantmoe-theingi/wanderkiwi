import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private favorites = new BehaviorSubject<number>(0);
  favoritesCount$ = this.favorites.asObservable();

  private savedItems: string[] = [];

  toggleFavorite(itemName: string) {
    const index = this.savedItems.indexOf(itemName);
    if (index > -1) {
      this.savedItems.splice(index, 1);
    } else {
      this.savedItems.push(itemName);
    }
    this.favorites.next(this.savedItems.length);
  }
}