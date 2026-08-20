import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutStoryMissionComponent } from './about-story-mission.component';

describe('AboutStoryMissionComponent', () => {
  let component: AboutStoryMissionComponent;
  let fixture: ComponentFixture<AboutStoryMissionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AboutStoryMissionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutStoryMissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
