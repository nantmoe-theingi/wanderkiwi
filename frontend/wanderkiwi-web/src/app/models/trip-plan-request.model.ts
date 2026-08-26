export interface TripPlanRequest {
  destination: string;
  startDate: string; // ISO date string
  endDate: string;   // ISO date string
  startTime: string,
  travellers: number;
  tripStyle: string;
  interests: string[];
  budget: string;
  transportMode: string;
}

