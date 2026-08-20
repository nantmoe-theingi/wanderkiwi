import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-article-hero',
  imports: [CommonModule, FormsModule],
  templateUrl: './article-hero.component.html',
  styleUrl: './article-hero.component.scss',
})
export class ArticleHeroComponent {
  searchQuery = '';
  @Output() searchChange = new EventEmitter<string>();

  onSearchChange() {
    this.searchChange.emit(this.searchQuery);
  }
}
