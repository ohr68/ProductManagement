import store from '@/store';
import type { ApiResponse } from '@/types/api_response';
import type { ProblemDetails } from '@/types/problem_details';
import axios, { AxiosError, type AxiosInstance, type AxiosRequestConfig, type AxiosResponse } from 'axios';

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

    async request<TData, TResponse>(method: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, data?: TData, params?: TData, config?: AxiosRequestConfig): Promise<ApiResponse<TResponse>> {
        try {

            const token = store.state.accessToken;

            const authHeaders = token ? { 'Authorization': `Bearer ${token}` } : {};
    
            const response: AxiosResponse<ApiResponse<TResponse>> = await this.axiosInstance({
                method,
                url,
                data,
                params,
                headers: {
                    ...authHeaders,
                    ...config?.headers,
                  },
                ...config,
            });

            return response.data;

        } catch (error) {
            throw this.handleError(error as AxiosError);
        }
    }

    async get<TData, TResponse>(url: string, params: TData, config?: AxiosRequestConfig): Promise<ApiResponse<TResponse>> {
        return this.request<TData, TResponse>('GET', url, undefined, params, config);
    }

    async post<TData, TResponse>(url: string, data: TData, config?: AxiosRequestConfig): Promise<ApiResponse<TResponse>> {
        return this.request<TData, TResponse>('POST', url, data, undefined, config);
    }

    async put<TData, TResponse>(url: string, data: TData, config?: AxiosRequestConfig): Promise<ApiResponse<TResponse>> {
        return this.request<TData, TResponse>('PUT', url, data, undefined, config);
    }

    async delete<TData, TResponse>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<TResponse>> {
        return this.request<TData, TResponse>('DELETE', url, undefined, undefined, config);
    }

    private handleError(error: AxiosError): ProblemDetails {
        if (axios.isAxiosError(error)) {
            if (error.response) {
                console.error('Error Response:', error.response);

                const problemDetails: ProblemDetails = error.response.data as ProblemDetails;

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