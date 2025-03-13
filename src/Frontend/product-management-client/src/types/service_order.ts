import { OrderStatus, OrderStatusDescriptions } from "@/enums/order_status";
import type { ListOrdersResult } from "./list_orders_result";

export class ServiceOrder {
    id: number;
    status: string;
    totalPrice: string;
    createdAt: string;
    completedAt: string | null

    constructor(order: ListOrdersResult) {
        this.id = order.id;
        this.status = OrderStatusDescriptions[order.status as OrderStatus];
        this.completedAt = order.completedAt;

        this.totalPrice = new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL',
        }).format(order.totalPrice);

        this.createdAt = this.GetFormattedDate(order.createdAt);
        this.completedAt = order.completedAt ? this.GetFormattedDate(order.completedAt) : '';
    }

    private GetFormattedDate(sourceDate: string): string {
        return new Date(sourceDate).toLocaleDateString('pt-BR', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
        });
    }
}