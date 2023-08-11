import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../_models/cartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private basketName = "webshopBasket"
  currentBasketSubject: BehaviorSubject<CartItem[]>;
  currentBasket: Observable<CartItem[]>


  
  
  constructor() { 
    // when constructor runs, get value stored in localStorage
    this.currentBasketSubject = new BehaviorSubject<CartItem[]>(
      JSON.parse(localStorage.getItem(this.basketName) || "[]")         // get basketName from localStorage OR create empty array if storage is empty
    );
    this.currentBasket = this.currentBasketSubject.asObservable(); 
   }

   get currentBasketValue(): CartItem[] {
    return this.currentBasketSubject.value;
  }

  saveBasket(basket: CartItem[]): void {
    localStorage.setItem(this.basketName, JSON.stringify(basket));
    this.currentBasketSubject.next(basket);         // updates basket value and broadcasts value to subscribers
  }



}


