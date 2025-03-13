import type { User } from "./user";

export interface AuthResult {
    user: User,
    accessToken: string
}