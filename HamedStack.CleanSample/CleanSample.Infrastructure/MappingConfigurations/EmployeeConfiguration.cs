using CleanSample.Domain.AggregateRoots;
using CleanSample.Domain.Enumerations;
using CleanSample.Framework.Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSample.Infrastructure.MappingConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");
        // builder.HasKey(e => e.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.ComplexProperty(e => e.FullName, ba =>
        {
            ba.Property(t => t.FirstName).HasColumnName("FirstName").HasMaxLength(100);
        });
        builder.ComplexProperty(e => e.FullName, ba =>
        {
            ba.Property(t => t.LastName).HasColumnName("LastName").HasMaxLength(100);
        });
        builder.Property(e => e.Gender).HasConversion<EnumerationValueConverter<Gender>>();

        builder.OwnsOne(e => e.Title, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Title").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.Street).HasColumnName("Street").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.City).HasColumnName("City").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.Country).HasColumnName("Country").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.PostalCode).HasColumnName("PostalCode").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.State).HasColumnName("State").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Phone, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Phone").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Fax, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Fax").HasMaxLength(100);
        });
        builder.ComplexProperty(e => e.Email, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Email").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.EmployeeStatus, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("EmployeeStatus").HasMaxLength(100);
        });
    }
}