export interface DestinationItem {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  rating: number;
  reviewCount: number;
  isPopular: boolean;
  regionId: number;
  regionName: string;
  islandId: number;
  islandName: string;
  categories: string[];
  attractions?: any[];
  subLocation?: string;
  bestTime?: string;
  activityLevel?: string;
  isBookmarked?: boolean;
}

export interface RegionItem {
  id: number;
  name: string;
  islandId: number;
  islandName: string;
}

export interface DestinationLandingData {
  popularDestinations: DestinationItem[];
  regions: RegionItem[];
  featuredAttractions: DestinationItem[];
}
