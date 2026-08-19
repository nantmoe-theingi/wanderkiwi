export interface TripPlanRequest {
  destination: string;
  startDate: string;
  endDate: string;
  travelers: string;
  tripStyle: string;
  interests: string[];
  budgetRange: string;
}

export interface ItineraryDay {
  dayNumber: number;
  title: string;
  description: string;
  tag: string;
  imageUrl: string;
}

export interface TripPlanResponse {
  durationLabel: string;
  locationLabel: string;
  days: ItineraryDay[];
  highlights: string[];
  bestTimeToVisit: string;
  weatherOutlook: string;
  travelTip: string;
}