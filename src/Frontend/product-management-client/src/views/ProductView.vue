<script setup lang="ts">
import Toast from 'primevue/toast';
import ProductsTable from '../components/ProductsTable.vue';
import store from '@/store';
</script>


<template>
    <Toast />
    <div v-if="store.state.user" class="flex flex-column align-items-stretch w-full min-h-full">
        <div class="flex justify-content-start py-5">
            <h1>Produtos</h1>
        </div>

        <ProductsTable />
    </div>
</template>