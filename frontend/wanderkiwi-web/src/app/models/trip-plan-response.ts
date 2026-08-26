import { TripDayItinerary } from "./trip-day-itinerary.model";

export interface TripPlanResponse {
  tripName: string;
  destinationName: string;
  totalDays: number;
  startDate: string;
  endDate: string;
  days: TripDayItinerary[];
}