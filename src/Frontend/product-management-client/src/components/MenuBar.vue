<script setup lang="ts">
import Menubar from 'primevue/menubar';
import Avatar from 'primevue/avatar';
import TieredMenu from 'primevue/tieredmenu';
import { ref, type PropType } from "vue";
import type { User } from '../types/user';

const items = ref([
    {
        label: 'Home',
        icon: 'pi pi-home',
        route: '/'
    },
    {
        label: 'Produtos',
        icon: 'pi pi-box',
        items: [
            {
                label: 'Consultar',
                route: '/products'
            },
            {
                label: 'Cadastrar',
                route: '/products/create'
            }]
    },
    {
        label: 'Ordens de Serviço',
        icon: 'pi pi-file-check',
        route: '/orders'
    }
]);

const props = defineProps({
    user: {
        type: Object as PropType<User>,
        required: true
    }
});

const emit = defineEmits<{ (e: 'logout', user: User): void }>();
const onLogout = () => emit('logout', props.user);

const userMenu = ref();
const userItems = ref([
    {
        label: 'Logout',
        icon: 'pi pi-sign-out',
        command: () => { onLogout(); }
    },
]);

const toggle = (event: Event) => {
    userMenu.value.toggle(event);
};

</script>

<template>
    <div class="card">
        <Menubar :model="items">
            <template #start>
                <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px"
                    fill="var(--p-primary-color)">
                    <path
                        d="M620-163 450-333l56-56 114 114 226-226 56 56-282 282Zm220-397h-80v-200h-80v120H280v-120h-80v560h240v80H200q-33 0-56.5-23.5T120-200v-560q0-33 23.5-56.5T200-840h167q11-35 43-57.5t70-22.5q40 0 71.5 22.5T594-840h166q33 0 56.5 23.5T840-760v200ZM480-760q17 0 28.5-11.5T520-800q0-17-11.5-28.5T480-840q-17 0-28.5 11.5T440-800q0 17 11.5 28.5T480-760Z" />
                </svg>
            </template>
            <template #item="{ item, props, hasSubmenu, }">
                <router-link v-if="item.route" v-slot="{ href, navigate }" :to="item.route" custom>
                    <a v-ripple :href="href" v-bind="props.action" @click="navigate">
                        <span :class="item.icon" />
                        <span>{{ item.label }}</span>
                    </a>
                </router-link>
                <a v-else v-ripple :href="item.url" :target="item.target" v-bind="props.action">
                    <span :class="item.icon" />
                    <span>{{ item.label }}</span>
                    <span v-if="hasSubmenu" class="pi pi-fw pi-angle-down" />
                </a>
            </template>
            <template #end>
                <div class="flex items-center gap-2">
                    <a class="cursor-pointer" label="Toggle" @click="toggle" aria-haspopup="true"
                        aria-controls="overlay_tmenu">
                        <Avatar icon="pi pi-user" class="mr-2" size="large"
                            style="background-color: var(--p-primary-color); color: var(--p-text-color)"
                            shape="circle" />
                        <TieredMenu ref="userMenu" id="overlay_tmenu" :model="userItems" popup />
                    </a>
                </div>
            </template>
        </Menubar>
    </div>
</template>