import { Role } from './role';

export class User {
  id: number;
  img: string;
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  branchId: string;
  supplierId: string;
  traineeId: string;
  role: Role;
  token?: string;
}
