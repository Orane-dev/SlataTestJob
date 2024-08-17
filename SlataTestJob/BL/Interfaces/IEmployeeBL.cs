using SlataTestJob.DAL.Models;

namespace SlataTestJob.BL.Interfaces
{
    public interface IEmployeeBL
    {
        Task<Employee?> GetEmployee(string fullName);
        Task<Employee> CreateEmployee(Employee employee);
        Task<IEnumerable<Employee>> SearchEmployeesByName(string name);
    }
}
