using System.Collections.Generic;
using System.Threading.Tasks;

using Api.DBModel;

namespace Api.Repository
{
    public interface IEmployeeRepository
    {     
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeAsync(int id);
        
        Task<Employee> InsertEmployeeAsync(Employee Employee);
        Task<bool> UpdateEmployeeAsync(Employee Employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}