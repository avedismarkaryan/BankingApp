import { Routes } from '@angular/router';
import { CustomerList } from './customer-list/customer-list';
import { Login } from './login/login';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'customers', component: CustomerList },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

//buraya "hangi path'te hangi component" eşleşmesini ekleyeceğiz.