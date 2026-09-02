import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-article-hero',
  imports: [CommonModule, FormsModule],
  templateUrl: './article-hero.component.html',
  styleUrl: './article-hero.component.scss',
})
export class ArticleHeroComponent {
  @Input() showSearch: boolean = true; // Defaults to showing search
  @Input() title: string = 'Travel Articles & Guides';
  @Input() subtitle: string = 'Inspiration, tips, and local insights to help you explore New Zealand like a local.';
  
  searchQuery: string = '';
  @Output() searchChange = new EventEmitter<string>();

  onSearchInput() {
    this.searchChange.emit(this.searchQuery);
  }
}
