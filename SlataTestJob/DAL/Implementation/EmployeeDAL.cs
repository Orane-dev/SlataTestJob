using Microsoft.EntityFrameworkCore;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DAL.Models;

namespace SlataTestJob.DAL.Implementation
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private ApplicationContext _appContext;

        public EmployeeDAL(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Employee?> GetEmployeeAsync(string fullName)
        {
            var employee = await _appContext.Employees.FirstOrDefaultAsync(x => x.Fullname == fullName);
            return employee;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _appContext.Employees.AddAsync(employee);
            await _appContext.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesByNameAsync(string fullName)
        {
            return await _appContext.Employees.Where(x => x.Fullname.ToLower().Contains(fullName.ToLower())).ToListAsync();
        }
    }
}
