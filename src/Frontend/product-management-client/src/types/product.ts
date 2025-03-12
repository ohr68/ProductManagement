import type { ListProductsResult } from "./list_products_result";

export class Product {
    id: number;
    name: string;
    description: string;
    price: string;
    createdAt: string;
  
    constructor(product: ListProductsResult) {
      this.id = product.id;
      this.name = product.name;
      this.description = product.description;
  
      this.price = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      }).format(product.price);
  
      this.createdAt = new Date(product.createdAt).toLocaleDateString('pt-BR', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      });
    }
  }