import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CartItem } from 'src/app/_models/cartItem';


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
  amount: number = 0;

  constructor(
    
  ) {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }




}
