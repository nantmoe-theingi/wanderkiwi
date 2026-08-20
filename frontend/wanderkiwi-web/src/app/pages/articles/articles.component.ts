import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ArticleGridComponent } from '../../shared/components/article-grid/article-grid.component';
import { ArticleHeroComponent } from '../../shared/components/article-hero/article-hero.component';
import { ArticleSidebarComponent } from '../../shared/components/article-sidebar/article-sidebar.component';
import { Article, ArticleCategory } from '../../models/article.model';
import { ArticleService } from '../../services/article.service';

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
  searchQuery = '';
  selectedFilter = 'All Articles';

  constructor(private articleService: ArticleService) {}

  ngOnInit() {
    this.articleService
      .getCategories()
      .subscribe((cats) => (this.categories = cats));
    this.articleService
      .getArticles()
      .subscribe((arts) => (this.articles = arts));
  }

  onSearch(query: string) {
    this.searchQuery = query.toLowerCase();
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
        article.category === this.selectedFilter ||
        article.category
          .toLowerCase()
          .includes(this.selectedFilter.toLowerCase());

      return matchesSearch && matchesCategory;
    });
  }
}
