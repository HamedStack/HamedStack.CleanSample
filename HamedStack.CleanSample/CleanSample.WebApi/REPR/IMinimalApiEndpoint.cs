// ReSharper disable IdentifierTypo
namespace CleanSample.WebApi.REPR;

public interface IMinimalApiEndpoint
{
    void HandleEndpoint(IEndpointRouteBuilder endpoint);
}