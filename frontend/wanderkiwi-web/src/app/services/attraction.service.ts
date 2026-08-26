import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Attraction } from '../models/attraction.model';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AttractionService {
  private apiUrl = `${environment.apiUrl}/attractions`;

  constructor(private http: HttpClient) {}

  getAttractions(search?: string, region?: string): Observable<Attraction[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (search) params.push(`search=${search}`);
    if (region) params.push(`region=${region}`);
    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }
    return this.http.get<Attraction[]>(url);
  }

  // Toggle between hardcoded mock data or live API data as you develop
  getDestinations(): Observable<Attraction[]> {
    // Return this when using live API:
    // return this.http.get<Attraction[]>(this.apiUrl);

    // Or keep your hardcoded data here until you're ready to connect!
    return of([
      { name: 'Queenstown', region: 'Otago', rating: 4.8, description: 'Adventure capital of the world with stunning lakes and mountains.', imageUrl: 'assets/images/queenstown.png' },
      { name: 'Milford Sound', region: 'Southland', rating: 4.9, description: 'Breathtaking fjord with towering cliffs and waterfalls.', imageUrl: 'assets/images/milford.png' },
      { name: 'Lake Tekapo', region: 'Canterbury', rating: 4.7, description: 'Stunning turquoise lake and world-famous starry skies.', imageUrl: 'assets/images/lake-tekapo.png' },
      { name: 'Rotorua', region: 'Bay of Plenty', rating: 4.6, description: 'Cultural experiences, geothermal wonders and adventure.', imageUrl: 'assets/images/rotorua.jpg' }
    ]);
    }
}