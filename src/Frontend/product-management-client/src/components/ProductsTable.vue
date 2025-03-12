<script setup lang="ts">
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import { useToast } from 'primevue/usetoast';
import { ref, onMounted } from 'vue';
import AxiosRequestHandler from '@/services/axios_request_handler';
import type { PaginatedRequest } from '@/types/paginated_request';
import type { ListProductsResult } from '@/types/list_products_result';
import type { ProblemDetails } from '@/types/problem_details';
import { Product } from '@/types/product';
import type { PaginatedListResult } from '@/types/paginated_list_result';
import ValidationError from '@/types/validation_error';
import type { ToastServiceMethods } from 'primevue';

const rowsPerPageOptions = ref<number[]>([10, 50, 100, 500]);
let currentPageIndex = 1;
let currentPageSize = 10;
let totalRecords = 0;
let isRequestInProgress = false;
const products = ref<Product[]>();
const errorMessage = ref<string>('');
const toast: ToastServiceMethods = useToast();

const handleProducts = async (pageIndex: number, pageSize: number) => {
    try {
        if (isRequestInProgress) return;
        isRequestInProgress = true;

        const handler = new AxiosRequestHandler('https://localhost:5003/api');

        const response = await handler.get<PaginatedRequest, ListProductsResult[]>('/products', {
            pageIndex,
            pageSize
        });

        const paginatedListResult: PaginatedListResult<ListProductsResult> = response as PaginatedListResult<ListProductsResult>;

        products.value = response.data!.map((product) => new Product(product));
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
            summary: 'Ocorreu uma falha ao consultar os produtos.',
            detail: errorMessage.value,
            life: 3000
        });
    }
    finally {
        isRequestInProgress = false;
    }
}

onMounted(() => {
    handleProducts(currentPageIndex, currentPageSize);
});

const onPageChange = (event: { first: number; rows: number }): void => {
    const newPageIndex = Math.floor(event.first / currentPageSize);

    if (newPageIndex !== currentPageIndex) {
        currentPageIndex = newPageIndex;
        handleProducts(newPageIndex, currentPageSize);
    }
};

const onRowsPerPageChange = async (newRowsPerPage: number): Promise<void> => {
    if (newRowsPerPage !== currentPageSize) {
        currentPageSize = newRowsPerPage;
        currentPageIndex = 1;
        await handleProducts(currentPageIndex, newRowsPerPage);
    }
};

</script>

<template>
    <div class="flex justify-content-center align-items-start">
        <div class="card w-full">
            <DataTable style="max-height: 300px;" :value="products" paginator :rows="currentPageSize"
                :total-records="totalRecords" :lazy="true" :first="1" @page="onPageChange"
                :rowsPerPageOptions="rowsPerPageOptions" @update:rows="onRowsPerPageChange" :responsive="true">
                <Column field="name" header="Nome" style="width: 25%"></Column>
                <Column field="description" header="Descrição" style="width: 25%"></Column>
                <Column field="price" header="Preço" style="width: 25%"></Column>
                <Column field="createdAt" header="Data Criação" style="width: 25%"></Column>
            </DataTable>
        </div>
    </div>
</template>