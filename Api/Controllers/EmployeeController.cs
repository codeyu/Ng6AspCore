using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.DBModel;
using Api.Repository;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        IEmployeeRepository _repo;
        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }
 

        // GET api/employee
        [HttpGet()]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(typeof(List<Employee>), 404)]
        public async Task<ActionResult> GetEmployees()
        {
            var employees = await _repo.GetEmployeesAsync();
            if (employees == null) {
              return NotFound();
            }
            return Ok(employees);
        }
        // GET api/employee/1
        [HttpGet("{id}", Name = "GetEmployeesRoute")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(Employee), 404)]
        public async Task<ActionResult> GetEmployees(int id)
        {
            var employee = await _repo.GetEmployeeAsync(id);
            if (employee == null) {
              return NotFound();
            }
            return Ok(employee);
        }

        // POST api/employee
        [HttpPost()]
        [ProducesResponseType(typeof(Employee), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> PostCustomer([FromBody]Employee employee)
        {
          if (!ModelState.IsValid) {
            return BadRequest(this.ModelState);
          }

          var newEmployee = await _repo.InsertEmployeeAsync(employee);
          if (newEmployee == null) {
            return BadRequest("Unable to insert employee");
          }
          return CreatedAtRoute("GetEmployeesRoute", new { id = newEmployee.EmployeeId}, newEmployee);
        }

        // PUT api/dataservice/employees/1
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<ActionResult> PutEmployee(int id, [FromBody]Employee employee)
        {
          if (!ModelState.IsValid) {
            return BadRequest(this.ModelState);
          }

          var status = await _repo.UpdateEmployeeAsync(employee);
          if (!status) {
            return BadRequest("Unable to update employee");
          }
          return Ok(status);
        }

        // DELETE api/employees/1
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 404)]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
          var status = await _repo.DeleteEmployeeAsync(id);
          if (!status) {
            return NotFound();
          }
          return Ok(status);
        }
    }
}