import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadComponent: () => import('./_components/home/home.component').then(_ => _.HomeComponent) },
  { path: 'products', loadComponent: () => import('./_components/product/product.component').then(_ => _.ProductComponent) },
  { path: 'product-details/:productId', loadComponent: () => import('./_components/product-details/product-details.component').then(_ => _.ProductDetailsComponent) },


  // otherwise redirect to home
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
