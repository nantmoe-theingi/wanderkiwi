import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { TripPlannerComponent } from './pages/trip-planner/trip-planner.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AboutUsComponent } from './pages/about-us/about-us.component';
import { ArticlesComponent } from './pages/articles/articles.component';
import { AllDestinationsComponent } from './pages/all-destinations/all-destinations.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            {
                path: '',
                component: HomeComponent
            },
            {
                path: 'all-destinations',
                component: AllDestinationsComponent
            },
            {
                path: 'trip-planner',
                component: TripPlannerComponent
            },
            {
                path: 'articles',
                component: ArticlesComponent
            },
            {
                path: 'about-us',
                component: AboutUsComponent
            }
        ]
    },
    {
        path: '**',
        redirectTo: ''
    }
];
