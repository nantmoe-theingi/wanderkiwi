import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DestinationLandingData, DestinationItem } from '../models/destination-item.model';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private baseUrl = 'http://localhost:5208/api'; // Adjust to your backend URL

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
}