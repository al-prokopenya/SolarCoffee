import axios from "axios";
import { IProductInventory } from "@/types/Product";
import { IShipment } from "@/types/Shipment";

/**
 * Inventory service
 * Provides UI BL associated with product inventory
 */
export class InventoryService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getInventory(): Promise<IProductInventory[]> {
    const result = await axios.get(`${this.API_URL}/inventory/`);

    return result.data;
  }

  public async updateInventoryQuantity(shipment: IShipment): Promise<any> {
    const result = await axios.patch(`${this.API_URL}/inventory/`, shipment);
    return result.data;
  }
}
