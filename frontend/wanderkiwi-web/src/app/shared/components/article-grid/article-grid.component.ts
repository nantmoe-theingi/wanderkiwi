import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Article } from '../../../models/article.model';

@Component({
  selector: 'app-article-grid',
  imports: [CommonModule, FormsModule],
  templateUrl: './article-grid.component.html',
  styleUrl: './article-grid.component.scss',
})
export class ArticleGridComponent {
  @Input() articles: Article[] = [];
  sortBy = 'latest';
}
