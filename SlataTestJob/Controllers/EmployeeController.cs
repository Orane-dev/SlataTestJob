using Microsoft.AspNetCore.Mvc;
using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Models;

namespace SlataTestJob.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private IEmployeeBL _employeeBL;

        public EmployeeController(IEmployeeBL employeeBL) 
        { 
            _employeeBL = employeeBL;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployee(string fullName)
        {
            try
            {
                var employee = await _employeeBL.GetEmployee(fullName);
                if (employee != null)
                {
                    return Ok();
                }
                return NotFound(new { Message = "Employee dont found!" });
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("/SearchEmployeesByName")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployeesByName(string fullName)
        {
            try
            {
                var employees = await _employeeBL.SearchEmployeesByName(fullName);
                if (!employees.Any())
                {
                    return NotFound(new { Message = "Employees not found" });
                }
                return Ok(employees);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody]Employee employee)
        {
            try
            {
                var createdEmployee = await _employeeBL.CreateEmployee(employee);
                return Ok(createdEmployee);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            
        }
    }
}
