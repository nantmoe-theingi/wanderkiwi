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
  // Extra fields for attractions if applicable
  subLocation?: string;
  bestTime?: string;
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