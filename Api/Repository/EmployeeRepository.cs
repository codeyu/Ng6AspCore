using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Api.DBModel;

namespace Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly HRContext _context;
        private readonly ILogger _logger;

        public EmployeeRepository(HRContext context, ILoggerFactory loggerFactory) {
          _context = context;
          _logger = loggerFactory.CreateLogger("EmployeeRepository");
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.OrderBy(c => c.LastName).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _context.Employees.SingleOrDefaultAsync(c => c.EmployeeId == id);
        }


        public async Task<Employee> InsertEmployeeAsync(Employee Employee)
        {
            _context.Add(Employee);
            try
            {
              await _context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
               _logger.LogError($"Error in {nameof(InsertEmployeeAsync)}: " + exp.Message);
            }

            return Employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee Employee)
        {
            _context.Employees.Attach(Employee);
            _context.Entry(Employee).State = EntityState.Modified;
            try
            {
              return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
               _logger.LogError($"Error in {nameof(UpdateEmployeeAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var Employee = await _context.Employees.SingleOrDefaultAsync(c => c.EmployeeId == id);
            _context.Remove(Employee);
            try
            {
              return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
               _logger.LogError($"Error in {nameof(DeleteEmployeeAsync)}: " + exp.Message);
            }
            return false;
        }

    }
}