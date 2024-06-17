export interface User {
    id: string;
    userName: string;
    biography: string;
    profilePicture: string  | null | undefined;
    city: string;
    postalCode: string;
    country: string;
    dateOfBirth: Date;
    email: string;
    phoneNumber: string;
  }
  