<script setup lang="ts">
import Toast from 'primevue/toast';
import ProductRegistration from '@/components/ProductRegistration.vue';
import store from '../store';
</script>

<template>
    <Toast />
    <div class="flex w-full justify-content-center align-items-center">
        <ProductRegistration v-if="store.state.user" />
    </div>
</template>