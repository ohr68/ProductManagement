<script setup lang="ts">
import Toast from 'primevue/toast';
import Card from 'primevue/card';
import LoginUser from '../components/LoginUser.vue';
import store from '../store';
</script>

<template>
  <Toast />
  <Card v-if="store.state.user">
    <template #title>Bem-vindo, {{ store.state.user.userName }}!</template>
    <template #content>
      <p class="m-0">
        Nesse projeto é possível efetuar ações como: criar e listar produtos, listar ordens de serviço e visualizar
        dados do seu perfil.
      </p>
    </template>
  </Card>
  <div class="flex w-full justify-content-center align-items-center">
    <LoginUser v-if="!store.state.user" />
  </div>
</template>
