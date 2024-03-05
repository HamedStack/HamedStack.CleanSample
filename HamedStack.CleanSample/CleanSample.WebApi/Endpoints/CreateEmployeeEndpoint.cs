using CleanSample.Application.Commands;
using CleanSample.WebApi.REPR;
using MediatR;

namespace CleanSample.WebApi.Endpoints;

public class CreateEmployeeEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/employee", async (CreateEmployeeCommand employeeCommand, IMediator mediator) =>
        {
            var output = await mediator.Send(employeeCommand);
            return Results.Ok(output.Value);
        });
    }
}