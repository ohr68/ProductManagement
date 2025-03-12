<script setup lang="ts">
import Toast from 'primevue/toast';
import TheWelcome from '../components/TheWelcome.vue'
import LoginUser from '../components/LoginUser.vue'
import store from '../store'
</script>

<template>
  <div>
    <Toast />
    <LoginUser v-if="!store.state.user"/>
  </div>
</template>
