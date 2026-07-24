import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { AboutComponent } from './pages/about/about.component';
import { TripPlannerComponent } from './pages/trip-planner/trip-planner.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'about',
        component: AboutComponent
    },
    {
        path: 'trip-planner',
        component: TripPlannerComponent
    },
    {
        path: '**',
        redirectTo: ''
    }
];
