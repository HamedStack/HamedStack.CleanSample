﻿using CleanSample.Application.Commands;
using CleanSample.Domain.Results;
using CleanSample.Framework.Application.Cqrs.Dispatchers;
using CleanSample.WebApi.REPR;

namespace CleanSample.WebApi.Endpoints;

public class CreateEmployeeEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/employee", CreateEmployeeEndpointHandler);
    }

    // Separated and static to make integration test easier.
    public static async Task<Result<int>> CreateEmployeeEndpointHandler(CreateEmployeeCommand employeeCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(employeeCommand);
        return output;
    }
}