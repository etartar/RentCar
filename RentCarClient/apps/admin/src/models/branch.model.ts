import { AddressModel } from "./address.model";
import { EntityModel } from "./entity.model";

export interface BranchModel extends EntityModel{
    name: string;
    address: AddressModel;
}

export const initialBranch: BranchModel = {
  id: '',
  name: '',
  address: {
    city: '',
    district: '',
    fullAddress: '',
    email: '',
    phoneNumber1: '',
    phoneNumber2: ''
  },
  isActive: true,
  createdAt: '',
  createdBy: '',
  createdFullName: ''
}
