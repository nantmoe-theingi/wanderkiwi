import { TripStopItinerary } from "./trip-stop-itinerary.model";

export interface TripDayItinerary {
  dayNumber: number;
  date: string;
  theme: string;
  stops: TripStopItinerary[];
}