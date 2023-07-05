

export class Product {
    id?: number;
    name?: string;
    price?: number;
    type?: string;
    photolink?: string;
}

export function resetProduct() {
    return { id: 0, name: '', price: 0, type: '', photolink: ''};
  }