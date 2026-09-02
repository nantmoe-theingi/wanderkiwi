import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Article } from '../../../models/article.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-article-grid',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './article-grid.component.html',
  styleUrl: './article-grid.component.scss',
})
export class ArticleGridComponent {
  @Input() articles: Article[] = [];
  @Input() selectedCategory: string = 'All Articles';
  sortBy = 'latest';

  currentPage = 1;
  pageSize = 6;

  ngOnChanges(changes: SimpleChanges) {
    // Reset back to page 1 whenever the category changes!
    if (changes['selectedCategory']) {
      this.currentPage = 1;
    }
  }

  get totalPages(): number {
    return Math.ceil(this.articles.length / this.pageSize) || 1;
  }

  // Returns sorted and paginated articles based on the dropdown selection
  get paginatedArticles(): Article[] {
    // 1. Create a shallow copy of the articles array to avoid mutating the original input reference
    let sorted = [...this.articles];

    // 2. Apply sorting logic
    if (this.sortBy === 'latest') {
      // Sort by date descending (newest first) based on the date string or ID
      sorted.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
    } else if (this.sortBy === 'popular') {
      // Sort by popularity (e.g., if you have a views property, or fallback to sorting by id/readTime length)
      // If your article model has a views or popularity field, use it here. Otherwise, we can simulate popularity by ID or read time:
      sorted.sort((a, b) => b.id - a.id); 
    }

    // 3. Safety check for pagination bounds
    if (this.currentPage > this.totalPages) {
      this.currentPage = 1;
    }

    // 4. Slice for pagination
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return sorted.slice(startIndex, startIndex + this.pageSize);
  }

  setPage(page: number | string) {
    if (typeof page === 'number') {
      this.currentPage = page;
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) this.currentPage++;
  }

  prevPage() {
    if (this.currentPage > 1) this.currentPage--;
  }
}
