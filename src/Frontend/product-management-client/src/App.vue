<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import MenuBar from './components/MenuBar.vue'
import store from './store'

</script>

<template>
  <div class="flex flex-column min-h-screen">
    <header>
      <MenuBar v-if="store.state.user" />
    </header>
    
    <main class="flex flex-grow-1 justify-content-center align-items-center w-full overflow-hidden">
      <RouterView />
    </main>
  </div>
</template>
<style scoped></style>
