import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../_models/cartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private basketName = "webshopBasket" // localstorage name where our bakset is placed

  currentBasketSubject: BehaviorSubject<CartItem[]>; // current cartitems stored in basket
  
  currentBasket: Observable<CartItem[]> // observable basket others can subscribe to

  constructor() { 
    // when constructor runs, get value stored in localStorage
    this.currentBasketSubject = new BehaviorSubject<CartItem[]>(
      JSON.parse(localStorage.getItem(this.basketName) || "[]")   // get basketName from localStorage OR create empty array if storage is empty
    );
    this.currentBasket = this.currentBasketSubject.asObservable(); 
   }

   get currentBasketValue(): CartItem[] {
    return this.currentBasketSubject.value;
  }

  saveBasket(basket: CartItem[]): void {
    localStorage.setItem(this.basketName, JSON.stringify(basket));
    this.currentBasketSubject.next(basket);         
    // updates basket value and broadcasts value to subscribers
  }

  addToBasket(item: CartItem): void {
    let productFound = false;
    let basket = this.currentBasketValue;

    // prevents dublicate items in the basket
    basket.forEach(basketItem => {            
      if (basketItem.productId == item.productId) {
        basketItem.quantity += item.quantity;
        productFound = true;
        if (basketItem.quantity <= 0) {
          this.removeItemFromBasket(item.productId);
        }
      }
    });
    if(!productFound) {
      basket.push(item);
    }
    this.saveBasket(basket);
  }

  removeItemFromBasket(productId: number): void {
    let basket = this.currentBasketValue;
    // check if product is present in the cart before removin from array with splice.

    for (let i = basket.length; i >= 0; i--) {
      if (basket[i].productId == productId) {
        basket.slice(i, 1);
      }
    }
    this.saveBasket(basket);
  }

  clearBasket(): void {
    let basket: CartItem[] = [];
    this.saveBasket(basket);
  }

  getBasketTotal(): number {
    let total: number = 0;
    this.currentBasketSubject.value.forEach(item => {
      total += item.price * item.quantity;
    });
    return total;
  }




}


