﻿using EventManagement.Application.Exceptions;
using EventManagement.Infrastuture.Logger;
using EventManagement.Shared.GlobalResponce;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
namespace EventManagement.Infrastuture.ExceptionHandler;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statuscode = HttpStatusCode.InternalServerError;
        Result<string> result = Result<string>.Failure(exception.Message);
        switch (exception)
        {
            case BadRequestException:
                statuscode = HttpStatusCode.BadRequest;
                result.Error = exception.Message;
                break;
            case ValidatonException validationException:
                statuscode = HttpStatusCode.BadRequest;
                result.Error = string.Join(", ", validationException.Errors);
                break;
            case NotFoundException:
                statuscode = HttpStatusCode.NotFound;
                result.Error = exception.Message;
                break;
            default:
                break;
        }

        context.Response.StatusCode = (int)statuscode;

        string? jsonResponse = JsonSerializer.Serialize(result);
        return context.Response.WriteAsync(jsonResponse);
    }
}
