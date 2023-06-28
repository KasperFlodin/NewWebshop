import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

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
    object-position: center;
  }

  .product-padding {
    padding: 2%;
  }

  `]
})
export class ProductComponent implements OnInit {
  products: Product[] | undefined;

  constructor(private productService:ProductService) { }

    ngOnInit(): void {
      this.productService.getAll().subscribe(data => { this.products = data;
      });
    }

}


