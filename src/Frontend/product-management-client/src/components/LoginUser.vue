<script setup lang="ts">
import InputGroup from 'primevue/inputgroup';
import InputGroupAddon from 'primevue/inputgroupaddon';
import InputText from 'primevue/inputtext';
import FloatLabel from 'primevue/floatlabel';
import Button from 'primevue/button';
import { useToast } from 'primevue/usetoast';
import { computed, ref } from 'vue';
import AxiosRequestHandler from '../services/axios_request_handler';
import type { ApiResponse } from '../types/api_response';
import type { AuthResult } from '../types/auth_result';
import store from '@/store';

const username = ref<string>('');
const password = ref<string>('');
const isLoginDisabled = computed(() => !username.value || !password.value);

const errorMessage = ref<string>('');
const toast = useToast();

const handleLogin = async () => {
    try {
        const handler = new AxiosRequestHandler();

        const response = await handler.post<ApiResponse<AuthResult>>('https://localhost:5001/api/auth/login', {
            username: username.value,
            password: password.value
        });

        const authResult: AuthResult = response.data;

        store.setUser(authResult.user, authResult.accessToken);

        toast.add({
            severity: 'success',
            summary: `Olá ${authResult.user.userName}`,
            detail: response.message,
            life: 3000
        });
    }
    catch (error) {
        if (error.detail) {
            errorMessage.value = error.detail;
        } else {
            errorMessage.value = 'Ocorreu um erro. Tente novamente.';
        }

        toast.add({ 
            severity: 'error', 
            summary: 'Ocorreu uma falha ao efetuar o login.', 
            detail: errorMessage.value, 
            life: 3000 
        });
    }
}

</script>

<template>
    <div class="flex flex-column row-gap-5">
        <InputGroup>
            <InputGroupAddon>
                <i class="pi pi-user"></i>
            </InputGroupAddon>
            <FloatLabel>
                <InputText id="username" v-model="username" />
                <label for="username">Username</label>
            </FloatLabel>
        </InputGroup>
        <InputGroup>
            <InputGroupAddon>
                <i class="pi pi-lock"></i>
            </InputGroupAddon>
            <FloatLabel>
                <InputText id="password" type="password" v-model="password" />
                <label for="password">Password</label>
            </FloatLabel>
        </InputGroup>

        <Button label="Login" :disabled="isLoginDisabled" @click="handleLogin" />
    </div>
</template>