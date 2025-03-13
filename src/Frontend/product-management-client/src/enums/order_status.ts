const OrderStatus = {
    Pending: 0,
    InProgress: 1,
    Completed: 2,
    Canceled: 3
} as const;

type OrderStatus = typeof OrderStatus[keyof typeof OrderStatus];

const OrderStatusDescriptions = {
    [OrderStatus.Pending]: "Pendente",
    [OrderStatus.InProgress]: "Em Andamento",
    [OrderStatus.Completed]: "Finalizada",
    [OrderStatus.Canceled]: "Cancelada"
};

export {
    OrderStatus,
    OrderStatusDescriptions
}