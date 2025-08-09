import { EntityModel } from "./entity.model";

export interface CategoryModel extends EntityModel{
    name: string;
}

export const initialCategory: CategoryModel = {
  id: '',
  name: '',
  isActive: true,
  createdAt: '',
  createdBy: '',
  createdFullName: ''
}
