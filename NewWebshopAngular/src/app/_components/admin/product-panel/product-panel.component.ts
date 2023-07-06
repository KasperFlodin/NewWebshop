import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product, resetProduct } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  standalone: true,
  selector: 'app-product-panel',
  templateUrl: './product-panel.component.html',
  imports: [CommonModule, FormsModule],
  styles: []
})
export class ProductPanelComponent implements OnInit{
  message: string ='';
  products: Product[] = [];
  product: Product = resetProduct();

  constructor(
    private productService: ProductService,
  ) { }

    ngOnInit(): void {
      this.productService.getAll().subscribe(t => this.products = t);
    }

    edit(product: Product): void {
      this.product = {
        id: product.id,
        name: product.name,
        price: product.price,
        type: product.type,
        photolink: product.photolink
      }
    }

    delete(product: Product): void {
      if (confirm('Er du sikker på du vil slette?')) {
        this.productService.delete(product.id).subscribe(() => {
          this.products = this.products.filter(x => x.id != product.id)
        });
      }
    }

    cancel(): void {
      this.message = '';
      this.product = resetProduct();
    }

    save(): void {
      this.message = '';

      if (this.product.id==0) {
        this.productService.create(this.product).subscribe({
          next: (x) => {
            this.products.push(x);
            this.product = resetProduct();
          },
          error: (err) => {
            this.message = Object.values(err.error.errors).join(', ');
          }
        });
      }
      else {
        this.productService.update(this.product).subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(', ');
          },
          complete: () => {



            this.productService.getAll().subscribe(x => this.products = x);
            this.product = resetProduct();
          }
        });
      }
    }
}
