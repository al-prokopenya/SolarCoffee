import axios from "axios";
import { ICustomer } from "@/types/Customer";
import { IServiceResponse } from "@/types/ServiceResponse";

/**
 * Customer service response
 * provides UI business logic with customers in our system
 */
export class CustomerService {
  private API_URL = process.env.VUE_APP_API_URL;

  public async getCustomers(): Promise<ICustomer[]> {
    const result = await axios.get(`${this.API_URL}/customer/`);
    return result.data;
  }

  public async addCustomer(
    newCustomer: ICustomer
  ): Promise<IServiceResponse<ICustomer>> {
    console.log("before post new customer");
    const result = await axios.post(`${this.API_URL}/customer/`, newCustomer);
    console.log("after post new customer");
    console.log("is success " + result.data.isSuccess);
    return result.data;
  }

  public async deleteCustomer(
    customerId: number
  ): Promise<IServiceResponse<boolean>> {
    const result = await axios.delete(`${this.API_URL}/customer/${customerId}`);

    return result.data;
  }
}
