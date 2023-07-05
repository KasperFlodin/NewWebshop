import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styles: [
  ]
})
export class AdminPanelComponent {
  loading = false;

  constructor( private router: Router) { }

  navToUsers() {
    this.router.navigate(['userpanel']);
  };

  navToProducts() {
    this.router.navigate(['productpanel']);
  };

}
