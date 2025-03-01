import { Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component.js';
import { DashboardComponent } from './shared/dashboard/dashboard.component.js';

export const routes: Routes = [
    { path: '', redirectTo: 'auth', pathMatch: 'full' }, // Redirige automáticamente a /auth
    { path: 'auth', component: AuthComponent },
    { path: 'dashboard', component: DashboardComponent }
];
