import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DestinationResultsComponent } from './destination-results.component';

describe('DestinationResultsComponent', () => {
  let component: DestinationResultsComponent;
  let fixture: ComponentFixture<DestinationResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DestinationResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DestinationResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
