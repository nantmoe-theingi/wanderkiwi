import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleCategory } from '../../../models/article.model';

@Component({
  selector: 'app-article-sidebar',
  imports: [CommonModule, FormsModule],
  templateUrl: './article-sidebar.component.html',
  styleUrl: './article-sidebar.component.scss',
})
export class ArticleSidebarComponent {
  @Input() categories: ArticleCategory[] = [];
  selectedCategory = 'All Articles';
  selectedTopic = '';
  email = '';

  popularTopics = [
    'South Island',
    'North Island',
    'Hiking',
    'Road Trips',
    'Family Travel',
    'Winter Travel',
  ];

  @Output() categoryChange = new EventEmitter<string>();

  selectCategory(name: string) {
    this.selectedCategory = name;
    this.categoryChange.emit(name);
  }

  selectTopic(topic: string) {
    this.selectedTopic = topic;
    this.categoryChange.emit(topic);
  }

  subscribe() {
    if (this.email) {
      console.log('Subscribed:', this.email);
      this.email = '';
    }
  }
}
