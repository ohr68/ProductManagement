<script setup lang="ts">
import InputGroup from 'primevue/inputgroup';
import InputGroupAddon from 'primevue/inputgroupaddon';
import InputText from 'primevue/inputtext';
import FloatLabel from 'primevue/floatlabel';
import Button from 'primevue/button';
import { useToast } from 'primevue/usetoast';
import { computed, ref } from 'vue';
import AxiosRequestHandler from '../services/axios_request_handler';
import type { AuthResult } from '../types/auth_result';
import store from '@/store';
import type { ProblemDetails } from '@/types/problem_details';
import type { AuthRequest } from '@/types/auth_request';

const username = ref<string>('');
const password = ref<string>('');
const isLoginDisabled = computed(() => !username.value || !password.value);

const errorMessage = ref<string>('');
const toast = useToast();

const handleLogin = async () => {
    try {
        const handler = new AxiosRequestHandler('https://localhost:5001/api');

        const response = await handler.post<AuthRequest, AuthResult>('/auth/login', {
            username: username.value,
            password: password.value
        });

        const authResult: AuthResult = response.data!;

        store.setUser(authResult.user, authResult.accessToken);

        toast.add({
            severity: 'success',
            summary: `Olá, ${authResult.user.userName}`,
            detail: response.message,
            life: 3000
        });
    }
    catch (error) {
        const details = error as ProblemDetails;

        if (details) {
            errorMessage.value = details.detail;
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
    <div class="flex flex-grow-1 justify-content-center align-items-center">
        <div class="flex flex-column row-gap-5">
            <div class="flex w-full justify-content-center py-4">
                <h1 class="text-4xl font-bold" style="color: var(--p-primary-color);">Bem-vindo!</h1>
            </div>
            <InputGroup>
                <InputGroupAddon>
                    <i class="pi pi-user"></i>
                </InputGroupAddon>
                <FloatLabel>
                    <InputText id="username" v-model="username" autofocus fluid />
                    <label for="username">Usuário</label>
                </FloatLabel>
            </InputGroup>
            <InputGroup>
                <InputGroupAddon>
                    <i class="pi pi-lock"></i>
                </InputGroupAddon>
                <FloatLabel>
                    <InputText id="password" type="password" v-model="password" fluid />
                    <label for="password">Senha</label>
                </FloatLabel>
            </InputGroup>
            <div class="flex flex-row w-full justify-content-center gap-2">
                <div class="flex my">
                    <span>Não possui um usuário?</span>
                </div>
                <div class="flex">
                    <a class="py-0" href="/register">Cadastrar </a>
                </div>
            </div>
            <Button label="Entrar" :disabled="isLoginDisabled" @click="handleLogin" />
        </div>
    </div>
</template>