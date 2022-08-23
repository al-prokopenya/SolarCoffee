export interface IAddress {
  id: number;
  country: string;
  state: string;
  city: string;
  zipCode: string;
  addressLine1: string;
  addressLine2: string;
  createdOn: Date;
  updatedOn?: Date;
}

export interface ICustomer {
  id: number;
  name: string;
  createdOn: Date;
  updatedOn?: Date;
  name: string;
  lastName: string;
  primaryAddress: IAddress;
}
