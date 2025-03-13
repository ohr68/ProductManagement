import type { ApiResponse } from './api_response';

export interface PaginatedListResult<T> extends ApiResponse<T> {
    currentPage: number,
    totalPages: number,
    totalCount: number
}