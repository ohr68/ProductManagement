<script setup lang="ts">
import Toast from 'primevue/toast';
import LoginUser from '../components/LoginUser.vue'
import store from '../store'
</script>

<template>
  <div class="flex w-full justify-content-center align-items-center">
    <Toast />
    <LoginUser v-if="!store.state.user" />
  </div>
</template>
