import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-categories',
  imports: [CommonModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent {
categories = [
    { name: 'Adventure', icon: '🏔️' },
    { name: 'Nature', icon: '🌿' },
    { name: 'Beach', icon: '🏖️' },
    { name: 'Food', icon: '🍲' },
    { name: 'Hiking', icon: '🥾' },
    { name: 'Culture', icon: '🏛️' },
    { name: 'Wildlife', icon: '🐧' },
    { name: 'Wine', icon: '🍷' }
  ];
}
