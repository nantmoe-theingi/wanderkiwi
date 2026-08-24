import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { TripPlanRequest, TripPlanResponse } from '../models/trip-planner.model';

@Injectable({
  providedIn: 'root'
})
export class TripPlannerService {
  private readonly apiUrl = 'http://localhost:5208/api/trips';
  private readonly temporaryOwnerId = 'local-user';

  constructor(private http: HttpClient) {}

  createTrip(request: TripPlanRequest): Observable<TripPlanResponse> {
    return this.http.post<TripPlanResponse>(this.apiUrl, {
      name: request.destination.trim(),
      ownerId: this.temporaryOwnerId,
      startDate: request.startDate,
      endDate: request.endDate,
      budgetRange: request.budgetRange,
      tripStyle: request.tripStyle,
      interests: request.interests
    });
  }
}
