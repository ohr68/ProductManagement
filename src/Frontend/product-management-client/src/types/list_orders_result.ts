export interface ListOrdersResult {
    id: number,
    status: number,
    totalPrice: number,
    createdAt: string,
    completedAt: string | null
}