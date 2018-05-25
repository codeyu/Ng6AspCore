using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.DBModel;
 

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        HRContext hrContext;
        public EmployeeController(HRContext hrContext)
        {
            this.hrContext = hrContext;
        }
 

        [HttpGet("GetAll")]
        public IActionResult GetAllEmployees()
        {
            var query = from emp in hrContext.Employees
                        join rm in hrContext.RoleAssignments on emp.EmployeeId equals rm.EmployeeId
                        where rm.Position == "Software developer"
                        select emp;
            return Ok(query.ToList());
        }
    }
}