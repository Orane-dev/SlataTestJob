using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Implementation;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DAL.Models;

namespace SlataTestJob.BL.Implementation
{
    public class EmployeeBL : IEmployeeBL
    {
        private IEmployeeDAL _employeeDAL;
        public EmployeeBL(IEmployeeDAL employeeDAL) 
        { 
            _employeeDAL = employeeDAL;
        }
        public async Task<Employee?> GetEmployee(string fullName)
        {
            return await _employeeDAL.GetEmployeeAsync(fullName);
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            return await _employeeDAL.CreateEmployeeAsync(employee);
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesByName(string name)
        {
            return await _employeeDAL.SearchEmployeesByNameAsync(name);
        }
    }
}
