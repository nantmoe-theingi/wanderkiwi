import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attraction } from '../models/attraction.model';

@Injectable({
  providedIn: 'root'
})
export class AttractionService {
  private apiUrl = 'http://localhost:5208/api/attractions'; 

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
}