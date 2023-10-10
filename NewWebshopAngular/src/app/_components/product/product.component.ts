import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CartItem } from 'src/app/_models/cartItem';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';
import { CartService } from 'src/app/_services/cart.service';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  standalone: true,
  selector: 'app-product',
  imports: [CommonModule, RouterModule],
  templateUrl: './product.component.html',
  styles: [`

  .card-img-top {
    width: 100%;
    height: 15vw;
    object-fit: cover;
    object-position: center top;
  }

  .product-padding {
    padding: 2%;
  }

  `]
})
export class ProductComponent implements OnInit {
  products: Product[] | undefined;
  productData!: Product;
  cartData!: CartItem;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private accountService: AccountService,
  ) { }

  ngOnInit(): void {
    this.productService.getAll().subscribe(data => { this.products = data;
    });
  }

  addToCart(): void {
    let item: CartItem = {
      productId: this.productData.id, 
      price: this.productData.price, 
      name: this.productData.name,
      quantity: 1 
    } as CartItem;
  
    this.cartService.addToBasket(item);
    this.cartData.quantity = this.cartData.quantity+1;
    // if NO user is logged in
    if(!this.accountService.currentUserValue) {
    }

    // if(this.productData != null) {
      
    //   // check if user is logged in
    //   // if(this.accountService.currentUserValue != null) {

    //   }
    //   console.log(this.productData);
    // }
  }

}


