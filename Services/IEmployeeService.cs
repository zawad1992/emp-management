using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HRMWeb.Models;
using HRMWeb.Data;
using HRMWeb.Data;
using HRMWeb.Models;

namespace HRMWeb.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int id);
    Task ImportXmlDataAsync(string xmlContent);
}

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(ApplicationDbContext context, ILogger<EmployeeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ImportXmlDataAsync(string xmlContent)
    {
        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            var employees = doc.SelectNodes("//Employee");
            if (employees != null)
            {
                foreach (XmlNode employeeNode in employees)
                {
                    var employee = new Employee
                    {
                        FirstName = employeeNode.SelectSingleNode("FirstName")?.InnerText ?? string.Empty,
                        LastName = employeeNode.SelectSingleNode("LastName")?.InnerText ?? string.Empty,
                        Division = employeeNode.SelectSingleNode("Division")?.InnerText ?? string.Empty,
                        Building = employeeNode.SelectSingleNode("Building")?.InnerText ?? string.Empty,
                        Title = employeeNode.SelectSingleNode("Title")?.InnerText ?? string.Empty,
                        Room = employeeNode.SelectSingleNode("Room")?.InnerText ?? string.Empty
                    };

                    await CreateEmployeeAsync(employee);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing XML data");
            throw;
        }
    }
}