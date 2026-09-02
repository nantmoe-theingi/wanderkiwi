import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Article, ArticleCategory } from '../../../models/article.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-article-sidebar',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './article-sidebar.component.html',
  styleUrl: './article-sidebar.component.scss',
})
export class ArticleSidebarComponent {
  @Input() categories: ArticleCategory[] = [];
  @Input() popularReads: Article[] = [];
  @Input() selectedCategory = 'All Articles';
  
  email = '';

  @Output() categoryChange = new EventEmitter<string>();

  selectCategory(name: string) {
    this.selectedCategory = name;
    this.categoryChange.emit(name);
  }

  subscribe() {
    if (this.email) {
      console.log('Subscribed:', this.email);
      this.email = '';
    }
  }
}
