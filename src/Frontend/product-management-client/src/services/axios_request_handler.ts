import type { ApiResponse } from '@/types/api_response';
import type { ProblemDetails } from '@/types/problem_details';
import axios, { type AxiosInstance, type AxiosRequestConfig, type AxiosResponse } from 'axios';

class AxiosRequestHandler {
    private axiosInstance: AxiosInstance;

    constructor(baseURL: string) {
        this.axiosInstance = axios.create({
            baseURL,
            timeout: 10000, // 10 seconds timeout
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async request<T>(method: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        try {
            const response: AxiosResponse<ApiResponse<T>> = await this.axiosInstance({
                method,
                url,
                data,
                ...config,
            });

            return response.data;

        } catch (error) {
            throw this.handleError(error);
        }
    }

    async get<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return this.request<T>('GET', url, undefined, config);
    }

    async post<T>(url: string, data: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return this.request<T>('POST', url, data, config);
    }

    async put<T>(url: string, data: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return this.request<T>('PUT', url, data, config);
    }

    async delete<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return this.request<T>('DELETE', url, undefined, config);
    }

    private handleError(error: any): ProblemDetails {
        if (axios.isAxiosError(error)) {
            if (error.response) {
                console.error('Error Response:', error.response);

                const problemDetails: ProblemDetails = error.response.data;

                return problemDetails;
            } else if (error.request) {
                console.error('Error Request:', error.request);
                return { title: 'Erro na requisição', detail: 'Nenhuma resposta foi recebida do servidor.', status: error.status ?? 400, type: 'RequestError' };
            }
        }
        console.error('Error Message:', error.message);
        return { title: 'Ocorreu um erro', detail: error.message, status: 500, type: 'RequestError' };
    }
}

export default AxiosRequestHandler;