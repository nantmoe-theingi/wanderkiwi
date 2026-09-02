import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ArticleGridComponent } from '../../shared/components/article-grid/article-grid.component';
import { ArticleHeroComponent } from '../../shared/components/article-hero/article-hero.component';
import { ArticleSidebarComponent } from '../../shared/components/article-sidebar/article-sidebar.component';
import { Article, ArticleCategory } from '../../models/article.model';
import { ArticleService } from '../../services/article.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-articles',
  imports: [
    CommonModule,
    ArticleHeroComponent,
    ArticleSidebarComponent,
    ArticleGridComponent,
  ],
  templateUrl: './articles.component.html',
  styleUrl: './articles.component.scss',
})
export class ArticlesComponent implements OnInit {
  articles: Article[] = [];
  categories: ArticleCategory[] = [];
  searchQuery: string = '';
  selectedFilter: string = 'All Articles';
  popularReads: Article[] = [];

  constructor(private articleService: ArticleService, private route: ActivatedRoute) {}

  ngOnInit() {
  this.route.queryParams.subscribe(params => {
    if (params['category']) {
      this.selectedFilter = params['category'];
    }
    this.loadAllArticles();
  });
}

  loadAllArticles() {
    this.articleService
      .getArticles('', '')
      .subscribe({
        next: (data) => {
          this.articles = data;
          this.popularReads = this.articles.slice(0, 3);

          // 1. Build category counts dynamically from the raw data
          const countsMap: { [key: string]: number } = {};
          data.forEach((article) => {
            const cat = article.category || 'Uncategorized';
            countsMap[cat] = (countsMap[cat] || 0) + 1;
          });

          // 2. Build the category array with dynamic counts
          const dynamicCategories: ArticleCategory[] = Object.keys(
            countsMap,
          ).map((catName) => ({
            name: catName,
            count: countsMap[catName],
            icon: this.getCategoryIcon(catName),
          }));

          // 3. Prepend "All Articles" with the total count matching data.length
          this.categories = [
            { name: 'All Articles', count: data.length, icon: '📁' },
            ...dynamicCategories,
          ];
        },
        error: (err) =>
          console.error('Error fetching articles from database', err),
      });
  }

  onSearch(query: string) {
    this.searchQuery = query ? query.toLowerCase().trim() : '';
  }

  onCategoryFilter(filter: string) {
    this.selectedFilter = filter;
  }

  get filteredArticles(): Article[] {
  return this.articles.filter((article) => {
    const matchesSearch =
      !this.searchQuery ||
      article.title.toLowerCase().includes(this.searchQuery) ||
      article.description.toLowerCase().includes(this.searchQuery);

    const matchesCategory =
      this.selectedFilter === 'All Articles' ||
      article.category?.trim().toLowerCase() === this.selectedFilter.trim().toLowerCase();

    return matchesSearch && matchesCategory;
  });
}

  private getCategoryIcon(category: string): string {
    switch (category.toLowerCase()) {
      case 'destinations':
        return '🏔️';
      case 'travel tips':
        return '💡';
      case 'road trips':
        return '🚗';
      case 'adventure':
        return '⚡';
      case 'off the beaten path':
        return '🌿';
      case 'food & wine':
        return '🍷';
      default:
        return '📁';
    }
  }
}
