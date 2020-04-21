import { UserRole } from './userRole';

export interface Role {
    id: number;
    name: string;
    userRoles: UserRole[];
}

