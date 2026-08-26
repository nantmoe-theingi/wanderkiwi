export interface TripStopItinerary {
  order: number;
  attractionId: number;
  attractionName: string;
  imageUrl: string;
  description: string;
  recommendedDuration: string;
  bestTime: string;
  timeSlot: string;
  driveTimeToNextMinutes: number;
  openingHoursNote: string;
  bookingNote: string;
  availabilityNote: string;
  latitude: number;
  longitude: number;
}