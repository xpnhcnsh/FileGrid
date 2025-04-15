using System;
using System.Net;

namespace FileGrid.Utils;

public class ApiResponse<TData>
{
    public required bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public TData? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string? ReturnUri { get; set; }

    public static ApiResponse<TData> Success(
        TData? data,
        string message,
        bool isSuccess = true,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        string? returnUri = null
        )
    {
        return new ApiResponse<TData>
        {
            IsSuccess = isSuccess,
            Data = data,
            Message = message,
            StatusCode = statusCode,
            ReturnUri = returnUri
        };
    }
    public static ApiResponse<TData> Failure(
        TData? data,
        string message,
        bool isSuccess = false,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        string? returnUri = null
        )
    {
        return new ApiResponse<TData>
        {
            IsSuccess = isSuccess,
            Data = data,
            Message = message,
            StatusCode = statusCode,
            ReturnUri = returnUri
        };
    }
}
