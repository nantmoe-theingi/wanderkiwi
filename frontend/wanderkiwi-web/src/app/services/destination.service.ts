import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DestinationLandingData, DestinationItem } from '../models/destination-item.model';
import { environment } from '../../environments/environment.development';
import { DestinationLookup } from '../models/destination-lookup.model';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private baseUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  // Fetches the initial landing page payload (popular destinations, regions, featured attractions)
  getLandingPageData(): Observable<DestinationLandingData> {
    return this.http.get<DestinationLandingData>(`${this.baseUrl}/Destinations/page`); // Adjust endpoint if needed
  }

  // Handles search queries like http://localhost:5208/api/Attractions/search?query=Hobbiton
  searchAttractions(query: string): Observable<DestinationItem[]> {
    const params = new HttpParams().set('query', query);
    return this.http.get<DestinationItem[]>(`${this.baseUrl}/Attractions/search`, { params });
  }

  getDestinationNames(): Observable<{ destinationNames: DestinationLookup }[]> {
  return this.http.get<{ destinationNames: DestinationLookup }[]>(`${this.baseUrl}/Destinations/names`);
}

  getPopularDestinations(): Observable<DestinationLookup[]> {
    return this.http.get<DestinationLookup[]>(`${this.baseUrl}/Destinations/popular`);
  }
}