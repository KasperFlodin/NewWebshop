import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
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
  cartProduct: Product = { id: 0, name: '', price: 0, type: '', photolink: '' };
  products: Product[] | undefined;
  productData!: Product;
  cartData!: CartItem;
  amount: number = 1;
  cartItems: CartItem[] = [];


  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.productService.getAll().subscribe(data => { this.products = data;});
    this.cartService.currentBasket.subscribe(x => this.cartItems = x);
    
    this.activatedRoute.paramMap.subscribe(params => {
      this.productService.getById(Number(params.get("productId"))).subscribe(x => this.cartProduct = x);
    })


    // let productId = this.activatedRoute.snapshot.paramMap.get('productId');
    // console.log(productId)

    // productId && this.productService.getById(productId).subscribe(r => {console.log(r); this.productData = r});
  }

  addToCart(item?: CartItem): void {
    if (item==null)
    item = {
      productId: this.cartProduct.id, 
      name: this.cartProduct.name,
      price: this.cartProduct.price, 
      quantity: this.amount
    } as CartItem;


  // addToCart(): void {
  //   let item: CartItem = {
  //     productId: this.productData.id, 
  //     name: this.productData.name,
  //     price: this.productData.price, 
  //     quantity: this.amount
  //   } as CartItem;

    this.cartService.addToBasket(item);


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


