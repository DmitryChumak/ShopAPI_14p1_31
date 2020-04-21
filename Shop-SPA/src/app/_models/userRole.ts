import { Role } from './role';
import { User } from './user';

export interface UserRole {
    userId: number;
    roleId: number;
    role?: Role;
    user?: User;
}
