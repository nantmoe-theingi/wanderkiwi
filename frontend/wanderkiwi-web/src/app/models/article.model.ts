export interface Article {
  id: number;
  title: string;
  description: string;
  contentJson?: string; 
  category: string;
  imageUrl: string;
  authorName: string;
  authorAvatar: string;
  date: string;
  readTime: string;
  viewsCount?: number;
}

export interface ArticleCategory {
  name: string;
  count: number;
  icon: string;
}