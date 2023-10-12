import { Component } from '@angular/core';
import { CartItem } from './_models/cartItem';
import { CartService } from './_services/cart.service';
import { Router } from '@angular/router';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  role: any;
  basket: CartItem[] = [];
  currentUser!: User | null;
  showAdminBoard? = false;

  constructor(
    private cartService: CartService,
    private router: Router,
    private accountService: AccountService,
  ) {
    this.accountService.currentUser.subscribe(x => this.currentUser = x);
    this.router.navigate(['/']);
  }

  ngOnInit(): void {
    this.currentUser = this.accountService.currentUserValue;
    this.role = this.currentUser?.role;
    this.showAdminBoard = this.role?.includes('Admin');
    this.cartService.currentBasket.subscribe(x => this.basket = x);
  }

  logout() {
    if (confirm('Er du sikker på du vil logge ud')) {
      this.accountService.logout();
      
      this.accountService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      })
    }
  }

}
