<script setup lang="ts">
import { RouterView, useRouter } from 'vue-router';
import { useToast } from 'primevue/usetoast';
import MenuBar from './components/MenuBar.vue';
import type { User } from '@/types/User';
import store from './store';

const toast = useToast();
const router = useRouter();

const onLogout = (user: User) => {
  store.clearUser();
  toast.add({
    severity: 'success',
    summary: `Até logo, ${user.userName}`,
    detail: 'Logout efetuado com sucesso.',
    life: 3000
  });

  setTimeout(() => {
    router.push('/');
  }, 500);
}
</script>

<template>
  <div class="flex flex-column w-full min-h-screen">
    <header>
      <MenuBar v-if="store.state.user" :user="store.state.user" @logout="onLogout" />
    </header>

    <main class="flex flex-grow-1 min-w-full min-h-full p-6 overflow-hidden">
      <RouterView />
    </main>
  </div>
</template>
<style scoped></style>
