<script setup lang="ts">
import { computed, ref } from 'vue';
import InputText from 'primevue/inputtext';
import InputNumber from 'primevue/inputnumber';
import Button from 'primevue/button';
import AxiosRequestHandler from '@/services/axios_request_handler';
import { useToast } from 'primevue';
import type { ProblemDetails } from '@/types/problem_details';
import ValidationError from '@/types/validation_error';
import type { CreateProductRequest } from '@/types/create_product_request';
import type { CreateProductResult } from '@/types/create_product_result';
import { useRouter } from 'vue-router';

const formData = ref({
    name: '',
    description: '',
    price: 1,
});

const errorMessage = ref<string>('');
const toast = useToast();
const router = useRouter();
const isSubmitDisabled = computed(() => !formData.value.name || !formData.value.description || !formData.value.price);

const onSubmit = async () => {
    try {
        const handler = new AxiosRequestHandler('https://localhost:5003/api');

        const response = await handler.post<CreateProductRequest, CreateProductResult>('/products', {
            name: formData.value.name,
            description: formData.value.description,
            price: formData.value.price
        });

        const createResult: CreateProductResult = response.data!;

        if (!createResult.id) {
            toast.add({
                severity: 'error',
                summary: `Ocorreu uma falha ao cadastrar o produto.`,
                detail: response.message,
                life: 3000
            });
        }

        toast.add({
            severity: 'success',
            summary: `Redirecionando para a tela de consulta.`,
            detail: response.message,
            life: 3000
        });

        redirectToIndex();
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

const redirectToIndex = () => {
    setTimeout(() => {
        router.push('/products');
    }, 3000);
};

</script>

<template>
    <div class="flex justify-content-center align-items-center mt-5">
        <div class="card shadow-3 border-round p-3" style="width: 400px;">
            <h2 class="text-3xl font-bold text-center" style="color: var(--p-primary-color);">Cadastrar Produto</h2>
            <form @submit.prevent="onSubmit">
                <div class="field grid">
                    <label for="name" class="col-12 md-3">Nome</label>
                    <div class="col-12 md-9">
                        <InputText id="name" v-model="formData.name" required class="inputtext-lg" autofocus fluid />
                    </div>
                </div>

                <div class="field grid">
                    <label for="description" class="col-12 md-3">Descrição</label>
                    <div class="col-12 md-9">
                        <InputText id="description" v-model="formData.description" required class="inputtext-lg"
                            fluid />
                    </div>
                </div>

                <div class="field grid">
                    <label for="price" class="col-12 md-3">Preço (R$)</label>
                    <div class="col-12 md-9">
                        <InputNumber id="price" v-model="formData.price" required class="inputtext-lg" :min="1"
                            :minFractionDigits="2" :maxFractionDigits="2" fluid />
                    </div>
                </div>

                <div class="field grid">
                    <div class="col-12 md-9 offset-md-3">
                        <Button label="Cadastrar" icon="pi pi-check" type="submit"
                            class="button-lg button-primary w-full" :disabled="isSubmitDisabled" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>