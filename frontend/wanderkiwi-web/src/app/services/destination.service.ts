import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private apiUrl = 'http://localhost:5208/api/Destinations'; // Adjust to your backend URL

  constructor(private http: HttpClient) {}

  searchDestinations(keyword?: string): Observable<any[]> {
    let params = new HttpParams();
    
    // Only pass the search parameter if the user typed something
    if (keyword) {
      params = params.set('search', keyword);
    }

    return this.http.get<any[]>(this.apiUrl, { params });
  }

  getFilteredDestinations(search?: string, region?: string, category?: string): Observable<any[]> {
    let params = new HttpParams();

    if (search) {
      params = params.set('search', search);
    }
    if (region && region !== 'All Regions') {
      params = params.set('region', region);
    }
    if (category && category !== 'All') {
      params = params.set('category', category);
    }

    return this.http.get<any[]>(this.apiUrl, { params }).pipe(
    map(destinations => destinations.map(dest => ({
      ...dest,
      // Convert comma-separated string from backend into a proper JavaScript array
      categories: typeof dest.categories === 'string' 
        ? dest.categories.split(',').map((c: string) => c.trim()) 
        : dest.categories
    })))
  );
  }
}