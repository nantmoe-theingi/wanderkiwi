import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Article, ArticleCategory } from '../models/article.model';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  private categories: ArticleCategory[] = [
    { name: 'All Articles', count: 56, icon: '🗂️' },
    { name: 'Travel Tips', count: 12, icon: '💡' },
    { name: 'Destinations', count: 18, icon: '📍' },
    { name: 'Outdoor Adventures', count: 14, icon: '⛰️' },
    { name: 'Accommodation', count: 6, icon: '🏨' },
    { name: 'Food & Drink', count: 8, icon: '🍲' },
    { name: 'Culture & History', count: 7, icon: '🏛️' },
    { name: 'Road Trips', count: 10, icon: '🚗' }
  ];

  private articles: Article[] = [
    {
      id: 1,
      title: '15 Must-Visit Places in New Zealand',
      description: 'From stunning fjords to geothermal wonders, discover the best places that should be on every traveller\'s list.',
      category: 'Destinations',
      imageUrl: 'https://images.unsplash.com/photo-1507692049790-de58290a4334?auto=format&fit=crop&w=600&q=80',
      authorName: 'Sarah Mitchell',
      authorAvatar: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?auto=format&fit=crop&w=100&q=80',
      date: 'May 18, 2025',
      readTime: '6 min read'
    },
    {
      id: 2,
      title: 'Packing List for New Zealand',
      description: 'What to pack for every season and adventure in New Zealand.',
      category: 'Travel Tips',
      imageUrl: 'https://images.unsplash.com/photo-1469854523086-cc02fe5d8800?auto=format&fit=crop&w=600&q=80',
      authorName: 'Tom Garcia',
      authorAvatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?auto=format&fit=crop&w=100&q=80',
      date: 'May 15, 2025',
      readTime: '4 min read'
    },
    {
      id: 3,
      title: 'The Ultimate South Island Road Trip',
      description: 'A 10-day itinerary covering glaciers, lakes, and coastal drives you\'ll never forget.',
      category: 'Road Trips',
      imageUrl: 'https://images.unsplash.com/photo-1518709268805-4e9042af9f23?auto=format&fit=crop&w=600&q=80',
      authorName: 'Emily Watson',
      authorAvatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?auto=format&fit=crop&w=100&q=80',
      date: 'May 12, 2025',
      readTime: '8 min read'
    },
    {
      id: 4,
      title: 'Top 10 Hot Springs in New Zealand',
      description: 'Relax and rejuvenate in these natural hot springs surrounded by breathtaking landscapes.',
      category: 'Outdoor Adventures',
      imageUrl: 'https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=600&q=80',
      authorName: 'Mike Reynolds',
      authorAvatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=100&q=80',
      date: 'May 10, 2025',
      readTime: '5 min read'
    },
    {
      id: 5,
      title: 'A Foodie\'s Guide to New Zealand',
      description: 'Local dishes, must-try foods, and where to find them.',
      category: 'Food & Drink',
      imageUrl: 'https://images.unsplash.com/photo-1555396273-367ea4eb4db5?auto=format&fit=crop&w=600&q=80',
      authorName: 'Jessica Lee',
      authorAvatar: 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?auto=format&fit=crop&w=100&q=80',
      date: 'May 8, 2025',
      readTime: '5 min read'
    },
    {
      id: 6,
      title: 'Understanding Māori Culture',
      description: 'A beginner\'s guide to New Zealand\'s rich Māori heritage and traditions.',
      category: 'Culture & History',
      imageUrl: 'https://images.unsplash.com/photo-1518709268805-4e9042af9f23?auto=format&fit=crop&w=600&q=80',
      authorName: 'Wiremu Taniora',
      authorAvatar: 'https://images.unsplash.com/photo-1522075469751-3a6694fb2f61?auto=format&fit=crop&w=100&q=80',
      date: 'May 5, 2025',
      readTime: '7 min read'
    }
  ];

  getCategories(): Observable<ArticleCategory[]> {
    return of(this.categories);
  }

  getArticles(): Observable<Article[]> {
    return of(this.articles);
  }
}