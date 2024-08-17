using SlataTestJob.DAL.Models;

namespace SlataTestJob.DAL.Interfaces
{
    public interface IEmployeeDAL
    {
        Task<Employee?> GetEmployeeAsync(string fullName);
        Task<IEnumerable<Employee>> SearchEmployeesByNameAsync(string fullName);
        Task<Employee> CreateEmployeeAsync(Employee employee);
    }
}
