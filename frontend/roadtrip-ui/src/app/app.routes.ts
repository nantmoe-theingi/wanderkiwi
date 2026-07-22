import { Routes } from '@angular/router';
import { TripsComponent } from './pages/trips/trips.component';
import { CreateTripComponent } from './pages/create-trip/create-trip.component';

export const routes: Routes = [
    { path: 'trips', component: TripsComponent },

  // optional default route
  { path: '', redirectTo: 'trips', pathMatch: 'full' },
  { path: 'trips/create', component: CreateTripComponent }
];