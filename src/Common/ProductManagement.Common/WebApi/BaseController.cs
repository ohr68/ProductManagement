using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Common.WebApi;

public class BaseController : ControllerBase
{
    protected IActionResult Ok<T>(T data) =>
        base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
        base.Ok(new PaginatedListResponse<T>()
        {
            Data = pagedList,
            CurrentPage = pagedList.GetCurrentPage,
            TotalPages = pagedList.GetTotalPages,
            TotalCount = pagedList.TotalCount,
            Success = true,
        });
}