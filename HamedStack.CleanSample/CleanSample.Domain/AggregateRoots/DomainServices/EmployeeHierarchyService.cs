// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable UnusedMember.Global
using CleanSample.Domain.Enumerations;

namespace CleanSample.Domain.AggregateRoots.DomainServices;
public class EmployeeHierarchyService
{
    private readonly List<Employee> _employees;

    public EmployeeHierarchyService(IEnumerable<Employee> employees)
    {
        _employees = employees.ToList() ?? throw new ArgumentNullException(nameof(employees));
    }

    public void AssignManager(int employeeId, int managerId)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == employeeId);
        var manager = _employees.FirstOrDefault(e => e.Id == managerId);

        if (employee == null)
        {
            throw new ArgumentException("Employee not found.", nameof(employeeId));
        }

        if (manager == null)
        {
            throw new ArgumentException("Manager not found.", nameof(managerId));
        }

        employee.Manager = manager;
        employee.ReportsTo = manager.Id;

        employee.EmployeeStatus = EmployeeStatus.Active;
    }

    public IEnumerable<Employee> GetSubordinates(int managerId)
    {
        return _employees.Where(e => e.ReportsTo == managerId).ToList();
    }

    public IEnumerable<Employee> GetManagerialChain(int employeeId)
    {
        var chain = new List<Employee>();
        var currentEmployee = _employees.FirstOrDefault(e => e.Id == employeeId);

        while (currentEmployee?.Manager != null)
        {
            chain.Add(currentEmployee.Manager);
            currentEmployee = currentEmployee.Manager;
        }

        return chain;
    }
}
