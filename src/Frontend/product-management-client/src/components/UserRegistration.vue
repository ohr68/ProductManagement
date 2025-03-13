<script setup lang="ts">
import { ref } from 'vue';
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';
import Button from 'primevue/button';
import AxiosRequestHandler from '@/services/axios_request_handler';
import { useToast } from 'primevue';
import type { ProblemDetails } from '@/types/problem_details';
import ValidationError from '@/types/validation_error';
import type { CreateUserRequest } from '@/types/create_user_request';
import type { CreateUserResult } from '@/types/create_user_result';
import { useRouter } from 'vue-router';

const formData = ref({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
});

const errorMessage = ref<string>('');
const toast = useToast();
const router = useRouter();

const onSubmit = async () => {
    try {
        const handler = new AxiosRequestHandler('https://localhost:5001/api');

        const response = await handler.post<CreateUserRequest, CreateUserResult>('/auth/register', {
            userName: formData.value.username,
            email: formData.value.email,
            password: formData.value.password
        });

        console.log(response.data);
        const createResult: CreateUserResult = response.data!;

        if (!createResult.id) {
            toast.add({
                severity: 'error',
                summary: `Ocorreu uma falha ao cadastrar o usuário.`,
                detail: response.message,
                life: 3000
            });
        }

        toast.add({
            severity: 'success',
            summary: `Redirecionando para a tela de login.`,
            detail: response.message,
            life: 3000
        });

        redirectToLogin();
    }
    catch (error) {
        const details = error as ProblemDetails;

        if (details) {
            if (details.status === 422) {
                const message = new ValidationError(details.detail).formattedMessage;
                errorMessage.value = message;
            } else {
                errorMessage.value = details.detail;
            }
        } else {
            errorMessage.value = 'Ocorreu um erro. Tente novamente.';
        }

        toast.add({
            severity: 'error',
            summary: 'Ocorreu uma falha ao efetuar o cadastro.',
            detail: errorMessage.value,
            life: 3000
        });
    }
};

const redirectToLogin = () => {
    setTimeout(() => {
        router.push('/');
    }, 4000);
};

</script>

<template>
    <div class="flex justify-content-center align-items-center mt-5">
        <div class="card shadow-3 border-round p-3" style="width: 400px;">
            <h2 class="text-3xl font-bold text-center" style="color: var(--p-primary-color);">Cadastrar Usuário</h2>
            <form @submit.prevent="onSubmit">
                <div class="field grid">
                    <label for="username" class="col-12 md-3">Usuário</label>
                    <div class="col-12 md-9">
                        <InputText id="username" v-model="formData.username" required class="inputtext-lg" autofocus
                            fluid />
                    </div>
                </div>

                <div class="field grid">
                    <label for="email" class="col-12 md-3">Email</label>
                    <div class="col-12 md-9">
                        <InputText id="email" v-model="formData.email" type="email" required class="inputtext-lg"
                            fluid />
                    </div>
                </div>

                <div class="field grid">
                    <label for="password" class="col-12 md-3">Senha</label>
                    <div class="col-12 md-9">
                        <Password id="password" v-model="formData.password" toggleMask required class="inputtext-lg"
                            promptLabel="Informe uma senha" weakLabel="Fraca" mediumLabel="Média" strongLabel="Forte"
                            fluid />
                    </div>
                </div>

                <div class="field grid">
                    <label for="confirmPassword" class="col-12 md-3">Confirme a Senha</label>
                    <div class="col-12 md-9">
                        <Password id="confirmPassword" v-model="formData.confirmPassword" toggleMask required
                            class="inputtext-lg" promptLabel="Informe uma senha" weakLabel="Fraca" mediumLabel="Média"
                            strongLabel="Forte" fluid />
                    </div>
                </div>

                <div class="field grid">
                    <div class="col-12 md-9 offset-md-3">
                        <Button label="Cadastrar" icon="pi pi-check" type="submit"
                            class="button-lg button-primary w-full" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>