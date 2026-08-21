export interface DestinationItem {
  id: number;
  name: string;
  region: string;
  subLocation?: string;
  rating: number;
  reviewCount: string;
  description: string;
  imageUrl: string;
  categories: string[];
  bestTimeToVisit: string;
  isPopular?: boolean;
}