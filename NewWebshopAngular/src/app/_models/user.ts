import { Role } from "./role";


export class User {
    id?: number;
    firstname?: string;
    lastname?: string;
    phone?: number;
    address?: string;
    city?: string;  
    zip?: number;
    email?: string;
    role?: Role;
}

export function resetUser() {
    return { id: 0, email: ''};
  }