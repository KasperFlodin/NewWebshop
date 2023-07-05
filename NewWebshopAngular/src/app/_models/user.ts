

export class User {
    id?: number;
    firstname?: string;
    lastname?: string;
    phone?: number;
    address?: string;
    city?: string;  
    zip?: number;
    email?: string;
}

export function resetUser() {
    return { id: 0, email: ''};
  }