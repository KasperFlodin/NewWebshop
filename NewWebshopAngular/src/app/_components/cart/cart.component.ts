import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CartItem } from 'src/app/_models/cartItem';
import { CartService } from 'src/app/_services/cart.service';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';



@Component({
  selector: 'app-cart',
  standalone: true,
  templateUrl: './cart.component.html',
  imports: [CommonModule, FormsModule],
  styles: [
  ]
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = []          // list of items in the cart
  amount: number = 1;

  constructor(
    public cartService: CartService,
    private router: Router,
    private accountService: AccountService,
    ) { }

  ngOnInit(): void {
    this.cartService.currentBasket.subscribe(x => this.cartItems = x);
  }

  clearCart(): void {
    this.cartService.clearBasket(); 
  }

  updateCart(): void {
    this.cartService.saveBasket(this.cartItems);
  }

  removeItem(item: CartItem): void {
    if (confirm(`Ønsker du at fjerne ${item.productId} ${item.name}?`)) {
        this.cartItems = this.cartItems.filter(x => x.productId != item.productId);
        this.cartService.saveBasket(this.cartItems);
    }
  }

  checkOut() {
    if(this.accountService.currentUserValue == null){
        this.router.navigate(['/login']);
    } else {
        this.router.navigate(['/checkout']);
    }
  }

}
