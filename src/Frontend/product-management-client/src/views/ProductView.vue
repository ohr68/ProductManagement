<script setup lang="ts">
import Toast from 'primevue/toast';
import Button from 'primevue/button';
import ProductsTable from '../components/ProductsTable.vue';
import { useRouter } from 'vue-router';
import store from '@/store';

const router = useRouter();

const handleCreate = () => {
    router.push('/products/create');
};
</script>


<template>
    <Toast />
    <div v-if="store.state.user" class="flex flex-column align-items-stretch w-full min-h-full">
        <div class="flex justify-content-start py-5 gap-4">
            <h1>Produtos</h1>
            <div class="flex my">
                <Button label="Cadastrar Novo Produto" @click="handleCreate" />
            </div>
        </div>

        <ProductsTable />
    </div>
</template>