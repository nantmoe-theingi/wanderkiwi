export interface DestinationItem {
  id: number;
  name: string;
  region: string;
  rating: number;
  reviewsCount: string;
  description: string;
  imageUrl: string;
  categories: string[];
  isPopular?: boolean;
}