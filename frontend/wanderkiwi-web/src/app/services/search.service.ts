import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SearchFilter } from '../models/search-filter.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private searchFilterSource = new BehaviorSubject<SearchFilter>({ keyword: '' });
  currentFilter$ = this.searchFilterSource.asObservable();

  updateSearch(filter: SearchFilter) {
    this.searchFilterSource.next(filter);
  }
}