import { Injectable } from '@angular/core';
import { DestinationItem } from '../models/destination-item.model';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {
  private storageKey = 'wanderkiwi_bookmarked_destinations';

  getStoredBookmarks(): number[] {
    const data = localStorage.getItem(this.storageKey);
    console.log('Retrieved bookmarks from localStorage:', data);
    return data ? JSON.parse(data) : [];
  }

  toggleBookmark(item: DestinationItem): boolean {
    let bookmarks = this.getStoredBookmarks();
    
    if (item.isBookmarked) {
      // Remove from bookmarks
      bookmarks = bookmarks.filter(id => id !== item.id);
      item.isBookmarked = false;
    } else {
      // Add to bookmarks
      if (!bookmarks.includes(item.id)) {
        bookmarks.push(item.id);
      }
      item.isBookmarked = true;
    }

    localStorage.setItem(this.storageKey, JSON.stringify(bookmarks));
    return item.isBookmarked;
  }

  isBookmarked(id: number): boolean {
    const bookmarks = this.getStoredBookmarks();
    return bookmarks.includes(id);
  }
}