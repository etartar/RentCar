import { EntityModel } from "./entity.model";

export interface ExtraModel extends EntityModel{
    name: string;
    price: number;
    description: string;
}

export const initialExtra: ExtraModel = {
  id: '',
  name: '',
  price: 0,
  description: '',
  isActive: true,
  createdAt: '',
  createdBy: '',
  createdFullName: ''
}
