//They are custom data types you made with type.
//They help your code by telling Angular/TypeScript what shape your data should look like.


//what the backend sends you after login/register.
export type User = {
    id: number;
    displayName: string;
    email: string;
    token:string;
    imageUrl?: string;
}

export type LoginCreds = {
    password: string;
    email: string;
}

export type RegisterCreds = {
    password: string;
    displayName: string;
    email: string;
}

