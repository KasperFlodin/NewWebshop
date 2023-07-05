import { Component } from '@angular/core';
import { Product, resetProduct } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  standalone: true,
  selector: 'app-product-panel',
  templateUrl: './product-panel.component.html',
  styles: [
  ]
})
export class ProductPanelComponent {
  message: string ='';
  products: Product[] = [];
  Product: Product = resetProduct();

  constructor(
    private productService: ProductService,
  ) { }

    ngOnInit(): void {
      this.productService.getAll().subscribe(t => this.Product = t);
    }

}
