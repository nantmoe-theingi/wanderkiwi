import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-hero',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss'
})
export class HeroComponent {
  searchQuery = '';
  popularTags = ['Queenstown', 'Milford Sound', 'Hobbiton', 'Rotorua', 'Wanaka'];

  onSearch() { console.log('Searching for:', this.searchQuery); }
  selectTag(tag: string) { this.searchQuery = tag; }

}
