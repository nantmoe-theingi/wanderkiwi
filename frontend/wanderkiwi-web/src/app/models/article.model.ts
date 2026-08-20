export interface Article {
  id: number;
  title: string;
  description: string;
  category: string;
  imageUrl: string;
  authorName: string;
  authorAvatar: string;
  date: string;
  readTime: string;
}

export interface ArticleCategory {
  name: string;
  count: number;
  icon: string;
}