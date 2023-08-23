import { Role } from "./role";


export class User {
    id?: number;
    firstName?: string;
    lastName?: string;
    phone?: number;
    address?: string;
    city?: string;  
    zip?: number;
    email?: string;
    password?: string;
    role?: Role;
}

export function resetUser() {
    return { id: 0, email: ''};
  }