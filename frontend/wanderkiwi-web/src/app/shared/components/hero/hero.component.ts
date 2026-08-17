import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-hero',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss'
})
export class HeroComponent {
  destination = '';
  popularSearches: string[] = [
    'Queenstown',
    'Milford Sound',
    'Hobbiton',
    'Rotorua',
    'Wanaka'
  ];

  onSearch() {
    // Implement search functionality here
    console.log('Searching for:', this.destination);
  }

}
