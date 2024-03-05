using CleanSample.Domain.Enumerations;
using CleanSample.Domain.ValueObjects;
using CleanSample.SharedKernel.Domain.AggregateRoots;

namespace CleanSample.Domain.AggregateRoots;

public class Employee : AggregateRoot<int>
{
    public FullName FullName { get; set; } = null!;
    public Gender Gender { get; set; } = null!;
    public Title? Title { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public Address? Address { get; set; } = null!;
    public Phone? Phone { get; set; }
    public Phone? Fax { get; set; }
    public Email Email { get; set; } = null!;
    public EmployeeStatus? EmployeeStatus { get; set; }
    public int? ReportsTo { get; set; }
    public virtual Employee? Manager { get; set; }

    public void SetHireDate(DateTime hireDate)
    {
        HireDate = hireDate;
    }
    public void SetTitle(string title)
    {
        Title = new Title(title);
    }
    public void SetPhone(string phone)
    {
        Phone = new Phone(phone);
    }
    public void SetFax(string fax)
    {
        Phone = new Phone(fax);
    }
    public void SetEmployeeStatus(EmployeeStatus employeeStatus)
    {
        EmployeeStatus = employeeStatus;
    }
}