using Microsoft.EntityFrameworkCore;

namespace CleanSample.SharedKernel.Infrastructure.Repositories;

public static class ExceptionExtensions
{
    public static bool IsDbUpdateConcurrencyException(this Exception ex)
    {
        return ex is DbUpdateConcurrencyException;
    }
}