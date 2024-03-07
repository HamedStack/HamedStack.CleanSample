using CleanSample.Application.Commands;
using CleanSample.Framework.Application.Cqrs.Dispatchers;
using CleanSample.WebApi.REPR;

namespace CleanSample.WebApi.Endpoints;

public class CreateEmployeeEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/employee", async (CreateEmployeeCommand employeeCommand, ICommandQueryDispatcher dispatcher) =>
        {
            var output = await dispatcher.Send(employeeCommand);
            return Results.Ok(output.Value);
        });
    }
}