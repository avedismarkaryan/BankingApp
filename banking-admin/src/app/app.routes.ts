import { Routes } from '@angular/router';
import { CustomerList } from './customer-list/customer-list';
import { Login } from './login/login';
import { FeatureManagement } from './feature-management/feature-management';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'customers', component: CustomerList },
  { path: 'features', component: FeatureManagement },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

//buraya "hangi path'te hangi component" eşleşmesini ekleyeceğiz.