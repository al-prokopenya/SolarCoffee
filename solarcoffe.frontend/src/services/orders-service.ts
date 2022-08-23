import axios from "axios";
import { IOrder } from "@/types/Order";

/**OrderService
 * Provides some BL for orders
 */
export class OrderService {
  private API_URL = process.env.VUE_APP_API_URL;

  public async getOrders(): Promise<IOrder[]> {
    const result = await axios.get(`${this.API_URL}/order/`);

    return result.data;
  }

  public async makeOrderCompleted(id: number): Promise<any> {
    const result = await axios.patch(`${this.API_URL}/order/complete/${id}`);

    return result.data;
  }
}
