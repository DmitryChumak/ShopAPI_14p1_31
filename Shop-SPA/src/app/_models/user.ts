import { UserRole } from './userRole';

export interface User {
    id: number;
    name: string;
    lastName: string;
    login: string;
    userRoles?: string[];
    password: string;
    token: string;
}
