export interface Attraction {
  id?: number;
  name: string;
  description: string;
  region: string;
  rating?: number;
  latitude?: number;
  longitude?: number;
  imageUrl: string;
}