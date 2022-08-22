import axios from "axios";
import { IProduct } from "@/types/Product";

/**
 * Product Service
 * Provides UI BL associated with products
 */
export class ProductService {
  API_URL = process.env.VUE_APP_API_URL;

  public async deleteProduct(id: number): Promise<any> {
    const result = await axios.patch(`${this.API_URL}/product/${id}`);
    return result.data;
  }

  public async saveNewProduct(newProduct: IProduct): Promise<any> {
    const result = await axios.post(`${this.API_URL}/product/`, newProduct);
    return result.data;
  }
}
