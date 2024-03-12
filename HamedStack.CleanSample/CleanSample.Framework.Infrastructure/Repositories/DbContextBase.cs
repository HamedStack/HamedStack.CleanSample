using System.Data;
using System.Linq.Expressions;
using CleanSample.Framework.Domain.AggregateRoots.Abstractions;
using CleanSample.Framework.Domain.Repositories;
using CleanSample.Framework.Infrastructure.Identity;
using CleanSample.Framework.Infrastructure.Interceptors;
using CleanSample.Framework.Infrastructure.Outbox;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace CleanSample.Framework.Infrastructure.Repositories;

public class DbContextBase : IdentityDbContext<ApplicationUser>, IUnitOfWork
{
    private readonly ILogger<DbContextBase> _logger;
    private IDbContextTransaction? _dbContextTransaction;

    internal DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    public DbContextBase(DbContextOptions options, ILogger<DbContextBase> logger)
        : base(options)
    {
        _logger = logger;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return _dbContextTransaction.GetDbTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction != null)
            await _dbContextTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_dbContextTransaction != null)
                await _dbContextTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Dispose();
                _dbContextTransaction = null;
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DomainEventOutboxInterceptor());
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        optionsBuilder.AddInterceptors(new AuditInterceptor());
        optionsBuilder.AddInterceptors(new PerformanceInterceptor(_logger));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetRowVersion(modelBuilder);
        SetSoftDeleteQueryFilter(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void SetRowVersion(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(IHaveRowVersion).IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property("RowVersion")
                .IsRowVersion();
        }
    }

    private static void SetSoftDeleteQueryFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(IHaveSoftDelete).IsAssignableFrom(entityType.ClrType)) continue;

            var entityClrType = entityType.ClrType;
            var parameter = Expression.Parameter(entityClrType, "e");
            var property = Expression.Property(parameter, nameof(IHaveSoftDelete.IsDeleted));
            var filterExpression = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);
            modelBuilder.Entity(entityClrType).HasQueryFilter(filterExpression);
        }
    }
}