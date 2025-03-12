<script setup lang="ts">
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import { useToast } from 'primevue/usetoast';
import { ref, onMounted } from 'vue';
import AxiosRequestHandler from '@/services/axios_request_handler';
import type { PaginatedRequest } from '@/types/paginated_request';
import type { ProblemDetails } from '@/types/problem_details';
import type { PaginatedListResult } from '@/types/paginated_list_result';
import ValidationError from '@/types/validation_error';
import type { ToastServiceMethods } from 'primevue';
import type { ListOrdersResult } from '@/types/list_orders_result';
import { ServiceOrder } from '@/types/service_order';

const rowsPerPageOptions = ref<number[]>([10, 50, 100, 500]);
let currentPageIndex = 1;
let currentPageSize = 10;
let totalRecords = 0;
let isRequestInProgress = false;
const orders = ref<ServiceOrder[]>();
const errorMessage = ref<string>('');
const toast: ToastServiceMethods = useToast();

const handleOrders = async (pageIndex: number, pageSize: number) => {
    try {
        if (isRequestInProgress) return;
        isRequestInProgress = true;

        const handler = new AxiosRequestHandler('https://localhost:5005/api');

        const response = await handler.get<PaginatedRequest, ListOrdersResult[]>('/orders', {
            pageIndex,
            pageSize
        });

        const paginatedListResult: PaginatedListResult<ListOrdersResult> = response as PaginatedListResult<ListOrdersResult>;

        orders.value = response.data!.map((order) => new ServiceOrder(order));
        currentPageIndex = paginatedListResult.currentPage;
        totalRecords = paginatedListResult.totalCount;
    }
    catch (error) {
        const details = error as ProblemDetails;

        if (details) {
            const message = new ValidationError(details.detail).formattedMessage;
            errorMessage.value = message;
        } else {
            errorMessage.value = 'Ocorreu um erro. Tente novamente.';
        }

        toast.add({
            severity: 'error',
            summary: 'Ocorreu uma falha ao consultar as ordens de serviço.',
            detail: errorMessage.value,
            life: 3000
        });
    }
    finally {
        isRequestInProgress = false;
    }
}

onMounted(() => {
    handleOrders(currentPageIndex, currentPageSize);
});

const onPageChange = (event: { first: number; rows: number }): void => {
    const newPageIndex = Math.floor(event.first / currentPageSize);

    if (newPageIndex !== currentPageIndex) {
        currentPageIndex = newPageIndex;
        handleOrders(newPageIndex, currentPageSize);
    }
};

const onRowsPerPageChange = async (newRowsPerPage: number): Promise<void> => {
    if (newRowsPerPage !== currentPageSize) {
        currentPageSize = newRowsPerPage;
        currentPageIndex = 1;
        await handleOrders(currentPageIndex, newRowsPerPage);
    }
};

</script>

<template>
    <div class="flex justify-content-center align-items-start">
        <div class="card w-full">
            <DataTable style="max-height: 300px;" :value="orders" paginator :rows="currentPageSize"
                :total-records="totalRecords" :lazy="true" :first="1" @page="onPageChange"
                :rowsPerPageOptions="rowsPerPageOptions" @update:rows="onRowsPerPageChange" :responsive="true">
                <Column field="id" header="Id" style="width: 15%"></Column>
                <Column field="status" header="Status" style="width: 25%"></Column>
                <Column field="totalPrice" header="Total" style="width: 15%"></Column>
                <Column field="createdAt" header="Data Criação" style="width: 20%"></Column>
                <Column field="completedAt" header="Completa Em" style="width: 20%"></Column>
            </DataTable>
        </div>
    </div>
</template>