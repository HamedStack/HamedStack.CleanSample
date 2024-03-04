using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using CleanSample.SharedKernel.Domain.AggregateRoots;

namespace CleanSample.Domain.AggregateRoots;

public class Employee : AggregateRoot<int>
{
    public FullName FullName { get; set; } = null!;
    public Gender Gender { get; set; } = null!;
    public Title Title { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public Address Address { get; set; } = null!;
    public Phone Phone { get; set; } = null!;
    public Phone? Fax { get; set; }
    public Email Email { get; set; } = null!;
    public int? ReportsTo { get; set; }
    public virtual Employee? Manager { get; set; }

}