import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiURL = environment.apiUrl + 'product';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiURL);
  }
  //https://localhost:7008/api/.Product/Product/2

  getById(productId: string): Observable<Product> {
    return this.http.get<Product>(this.apiURL + '/'+ 'Product' + '/' + productId);
  }

  create(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiURL, product);
  }

  update(product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiURL}/${product.id}`, product);
  }

  delete(productId: number | undefined): Observable<Product> {
    return this.http.delete<Product>(`${this.apiURL}/${productId}`);
  }

}
