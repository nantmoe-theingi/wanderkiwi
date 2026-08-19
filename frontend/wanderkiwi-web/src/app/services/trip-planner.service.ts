import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TripPlanRequest, TripPlanResponse } from '../models/trip-planner.model';

@Injectable({
  providedIn: 'root'
})
export class TripPlannerService {

  generateTrip(request: TripPlanRequest): Observable<TripPlanResponse> {
    // Hardcoded response matching your design preview
    const mockResponse: TripPlanResponse = {
      durationLabel: '7 Days / 6 Nights',
      locationLabel: request.destination || 'Queenstown, New Zealand',
      days: [
        {
          dayNumber: 1,
          title: 'Arrive in Queenstown',
          description: 'Arrive and settle in. Explore the beautiful Queenstown waterfront.',
          tag: 'Relaxation',
          imageUrl: 'assets/images/queenstown.jpg'
        },
        {
          dayNumber: 2,
          title: 'Adventure in the Remarkables',
          description: 'Hike the Ben Lomond Track and enjoy stunning panoramic views.',
          tag: 'Adventure',
          imageUrl: 'assets/images/milford.jpg'
        },
        {
          dayNumber: 3,
          title: 'Milford Sound Day Trip',
          description: 'Full-day tour to Milford Sound with cruise and scenic stops.',
          tag: 'Nature',
          imageUrl: 'assets/images/rotorua.jpg'
        },
        {
          dayNumber: 4,
          title: 'Gibbston Valley Wine Region',
          description: 'Discover world-class wines and local produce in Gibbston Valley.',
          tag: 'Food & Wine',
          imageUrl: 'assets/images/hobbiton.jpg'
        }
      ],
      highlights: [
        'Stunning hikes',
        'Scenic drives',
        'Wildlife experiences',
        'Local cuisine & wine'
      ],
      bestTimeToVisit: 'May offers great weather and fewer crowds.',
      weatherOutlook: '15°C - 18°C average with mixed sun & clouds.',
      travelTip: 'Book Milford Sound tours in advance for best availability.'
    };

    return of(mockResponse);
  }
}