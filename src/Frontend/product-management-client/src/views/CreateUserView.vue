<script setup lang="ts">
import Toast from 'primevue/toast';
import UserRegistration from '@/components/UserRegistration.vue';
import store from '../store';

</script>

<template>
    <Toast />
    <div class="flex w-full justify-content-center align-items-center">
        <UserRegistration v-if="!store.state.user" />
    </div>
</template>