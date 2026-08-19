import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CategoryItem } from '../../../models/category.model';

@Component({
  selector: 'app-categories',
  imports: [CommonModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent {
categories: CategoryItem[] = [
    { name: 'Adventure', icon: '🏔️' },
    { name: 'Nature', icon: '🌿' },
    { name: 'Beach', icon: '🏖️' },
    { name: 'Food', icon: '🍲' },
    { name: 'Hiking', icon: '🥾' },
    { name: 'Culture', icon: '🏛️' },
    { name: 'Wildlife', icon: '🐧' },
    { name: 'Wine', icon: '🍷' }
  ];

  selectCategory(category: CategoryItem) {
    console.log('Selected category:', category.name);
    // later broadcast this via a shared service to filter attractions!
  }
}
