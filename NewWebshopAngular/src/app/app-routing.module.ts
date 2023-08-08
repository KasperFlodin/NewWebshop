import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadComponent: () => import('./_components/home/home.component').then(_ => _.HomeComponent) },
  { path: 'products', loadComponent: () => import('./_components/product/product.component').then(_ => _.ProductComponent) },
  { path: 'product-details/:productId', loadComponent: () => import('./_components/product-details/product-details.component').then(_ => _.ProductDetailsComponent) },
  { path: 'adminpanel', loadComponent: () => import('./_components/admin/admin-panel/admin-panel.component').then(_ => _.AdminPanelComponent) },
  { path: 'productpanel', loadComponent: () => import('./_components/admin/product-panel/product-panel.component').then(_ => _.ProductPanelComponent) },
  { path: 'userpanel', loadComponent: () => import('./_components/admin/user-panel/user-panel.component').then(_ => _.UserPanelComponent) },
  { path: 'login', loadComponent: () => import('./_components/account/login/login.component').then(_ => _.LoginComponent) },
  { path: 'register', loadComponent: () => import('./_components/account/register/register.component').then(_ => _.RegisterComponent) },
  
  
  // otherwise redirect to home
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
