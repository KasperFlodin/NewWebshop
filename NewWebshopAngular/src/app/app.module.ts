import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponent } from './_components/product/product.component';
import { ProductDetailsComponent } from './_components/product-details/product-details.component';
import { HomeComponent } from './_components/home/home.component';
import { UserPanelComponent } from './_components/admin/user-panel/user-panel/user-panel.component';
import { ProductPanelComponent } from './_components/admin/admin-product/product-panel/product-panel/product-panel.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ProductComponent,
    HttpClientModule,
    ProductDetailsComponent,
    HomeComponent,
    UserPanelComponent,
    ProductPanelComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
