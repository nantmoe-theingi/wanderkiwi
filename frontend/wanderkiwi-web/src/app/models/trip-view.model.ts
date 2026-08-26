export interface AttractionInfo {
  name: string;
  location: string;
  latitude: number;
  longitude: number;
}

export interface DrivingInfo {
  durationMinutes: number;
  distanceKm: number;
  isRealRoute: boolean;
}

export interface TripStop {
  order: number;
  type: 'activity' | 'travel' | 'meal' | 'free_time';
  startTime: string;
  endTime: string;
  title: string;
  description: string;
  attraction: AttractionInfo;
  driving: DrivingInfo;
  durationMinutes: number;
  weatherDependent: boolean;
  isFromDatabase: boolean;
  attractionId: number | null;
  imageUrl: string | null;
  latitude: number | null;
  longitude: number | null;
  dataSource: string;
}

export interface TripDay {
  dayNumber: number;
  date: string;
  theme: string;
  summary: string;
  stops: TripStop[];
}

export interface TripResponse {
  tripName: string;
  destinationName: string;
  startDate: string;
  endDate: string;
  totalDays: number;
  travelers: number;
  tripStyle: string;
  interests: string[];
  budget: string;
  transportMode: string;
  summary: string;
  days: TripDay[];
}