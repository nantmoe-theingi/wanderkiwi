// export interface TripPlanRequest {
//   destination: string;
//   startDate: string;
//   endDate: string;
//   travelers: string;
//   tripStyle: string;
//   interests: string[];
//   budgetRange: string;
// }

export interface TripStop {
  id: number;
  attractionId: number | null;
  name: string;
  imageUrl: string | null;
  sortOrder: number;
  plannedDurationMinutes: number | null;
  notes: string | null;
}

export interface ItineraryDay {
  id: number;
  dayNumber: number;
  date: string;
  stops: TripStop[];
}

// export interface TripPlanResponse {
//   id: number;
//   ownerId: string;
//   name: string;
//   startDate: string;
//   endDate: string;
//   budgetRange: string;
//   tripStyle: string;
//   days: ItineraryDay[];
// }
