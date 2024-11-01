using Microsoft.AspNetCore.Mvc;
using HRMWeb.Models;
using HRMWeb.Services;

namespace HRMWeb.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employees");
            TempData["Error"] = "Error loading employees";
            return View(new List<Employee>());
        }
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employee details");
            TempData["Error"] = "Error loading employee details";
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,Division,Building,Title,Room")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _employeeService.CreateEmployeeAsync(employee);
                TempData["Success"] = "Employee created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                ModelState.AddModelError("", "Error creating employee");
            }
        }
        return View(employee);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Division,Building,Title,Room")] Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(employee);
                TempData["Success"] = "Employee updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee");
                ModelState.AddModelError("", "Error updating employee");
            }
        }
        return View(employee);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _employeeService.DeleteEmployeeAsync(id);
            TempData["Success"] = "Employee deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee");
            TempData["Error"] = "Error deleting employee";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportXml(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Please select a file to import";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            using var reader = new StreamReader(file.OpenReadStream());
            string xmlContent = await reader.ReadToEndAsync();
            await _employeeService.ImportXmlDataAsync(xmlContent);
            TempData["Success"] = "XML data imported successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing XML");
            TempData["Error"] = "Error importing XML data";
        }

        return RedirectToAction(nameof(Index));
    }
}