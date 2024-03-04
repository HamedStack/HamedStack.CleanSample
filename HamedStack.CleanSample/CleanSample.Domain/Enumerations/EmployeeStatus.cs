using CleanSample.SharedKernel.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSample.Domain.Enumerations;

public class EmployeeStatus : Enumeration<EmployeeStatus>
{
    private EmployeeStatus()
    {

    }

    public static readonly EmployeeStatus Active = new("Active", 1, "Indicates that the employee is currently active within the company.");
    public static readonly EmployeeStatus Inactive = new("Inactive", 2, "Indicates that the employee is currently not active within the company.");
    public static readonly EmployeeStatus OnLeave = new("OnLeave", 3, "Indicates that the employee is currently on leave.");
    public static readonly EmployeeStatus Retired = new("Retired", 4, "Indicates that the employee has retired from their position.");

    public EmployeeStatus(string name, int value, string? description = "") : base(name, value, description)
    {
    }
}