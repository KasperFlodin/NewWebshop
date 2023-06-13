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

  getById(productId: number): Observable<Product> {
    return this.http.get<Product>('${this.apiUrl}/${productId}');
  }

}
