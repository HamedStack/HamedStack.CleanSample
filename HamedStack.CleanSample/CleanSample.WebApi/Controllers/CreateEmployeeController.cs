using CleanSample.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateEmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CreateEmployeeController> _logger;

        public CreateEmployeeController(IMediator mediator, ILogger<CreateEmployeeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost(Name = "InsertEmployee")]
        public async Task<IResult> InsertEmployee(CreateEmployeeCommand employeeCommand)
        {
            try
            {
                var result = await _mediator.Send(employeeCommand);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting employee");
                return Results.Problem("An error occurred while inserting the employee.");
            }
        }
    }
}
