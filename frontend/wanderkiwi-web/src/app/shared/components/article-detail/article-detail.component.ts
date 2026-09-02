import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ArticleService } from '../../../services/article.service';
import { Article, ArticleCategory } from '../../../models/article.model';
import { CommonModule } from '@angular/common';
import { ArticleSidebarComponent } from '../article-sidebar/article-sidebar.component';
import { ArticleHeroComponent } from '../article-hero/article-hero.component';

@Component({
  selector: 'app-article-detail',
  imports: [CommonModule, RouterLink, ArticleSidebarComponent, ArticleHeroComponent],
  templateUrl: './article-detail.component.html',
  styleUrl: './article-detail.component.scss',
})
export class ArticleDetailComponent implements OnInit {
  article: Article | null = null;
  loading: boolean = true;
  parsedContent: any = null;

  allArticles: Article[] = [];
  categories: ArticleCategory[] = [];
  popularReads: Article[] = [];

  prevArticle: Article | null = null;
  nextArticle: Article | null = null;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService,
    private router: Router,
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        this.loadArticleData(+idParam);
      }
    });
  }

  loadArticleData(articleId: number) {
    this.loading = true;
    this.articleService.getArticles('', '').subscribe({
      next: (articles) => {
        this.allArticles = articles;
        this.article = articles.find((a) => a.id === articleId) || null;
        this.loading = false;

        if (this.article) {
          try {
            this.parsedContent =
              typeof (this.article as any).contentJson === 'string'
                ? JSON.parse((this.article as any).contentJson)
                : (this.article as any).contentJson;
          } catch (e) {
            console.error('Error parsing JSON', e);
          }

          this.buildCategories(articles);
          this.popularReads = articles.slice(0, 3);

          const currentIndex = articles.findIndex((a) => a.id === articleId);
          this.prevArticle =
            currentIndex > 0 ? articles[currentIndex - 1] : null;
          this.nextArticle =
            currentIndex < articles.length - 1
              ? articles[currentIndex + 1]
              : null;
        }
      },
      error: (err) => {
        console.error('Error loading articles', err);
        this.loading = false;
      },
    });
  }

  buildCategories(articles: Article[]) {
    const countsMap: { [key: string]: number } = {};
    articles.forEach((art) => {
      const cat = art.category || 'Uncategorized';
      countsMap[cat] = (countsMap[cat] || 0) + 1;
    });

    const dynamicCategories: ArticleCategory[] = Object.keys(countsMap).map(
      (catName) => ({
        name: catName,
        count: countsMap[catName],
        icon: this.getCategoryIcon(catName),
      }),
    );

    this.categories = [
      { name: 'All Articles', count: articles.length, icon: '📁' },
      ...dynamicCategories,
    ];
  }

  // Handle category selection from the detail page sidebar
  onCategorySelected(categoryName: string) {
    // Navigate back to the articles page, passing the category as a query parameter
    this.router.navigate(['/articles'], {
      queryParams: { category: categoryName },
    });
  }

  private getCategoryIcon(category: string): string {
    switch (category.toLowerCase()) {
      case 'destinations':
        return '📍';
      case 'travel tips':
        return '💡';
      case 'road trips':
        return '🚗';
      case 'adventure':
      case 'outdoor adventures':
        return '🏔️';
      case 'accommodation':
        return '🏨';
      case 'food & wine':
      case 'food & drink':
        return '🍷';
      case 'culture & history':
        return '🏛️';
      default:
        return '📁';
    }
  }
}
