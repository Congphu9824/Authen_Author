using API.Repositories;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployController : ControllerBase
    {
        private readonly IRepEmploy _IrepEmploy;
        public EmployController(IRepEmploy repEmploy)
        {
            _IrepEmploy = repEmploy;
        }

        [HttpGet]
        public async  Task<IActionResult> Index() 
        {
            var result = await _IrepEmploy.GetAll();
            var BaseUrl = $"{Request.Scheme}://{Request.Host}";
            foreach (var item in result) 
            {
                if (!string.IsNullOrEmpty(item.Image))
                {
                    item.Image = BaseUrl + item.Image;
                }
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Employee employee, IFormFile? file)
        {
            if(file != null)
            {
                employee.Image = await _IrepEmploy.umploadImage(file);
            }
            await _IrepEmploy.Create(employee);
            return Ok(employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromForm] Employee employee, IFormFile? file, int id)
        {
           var ExistingEmploy = await _IrepEmploy.GetById(id);
            if (ExistingEmploy != null) {
                if (file != null)
                {
                    employee.Image = await _IrepEmploy.umploadImage(file);
                }
                else 
                {
                    employee.Image = ExistingEmploy.Image;
                }

                ExistingEmploy.Name = employee.Name;
                ExistingEmploy.Location = employee.Location;
                ExistingEmploy.Status = employee.Status;
                ExistingEmploy.Email = employee.Email;
                ExistingEmploy.Address = employee.Address;
                ExistingEmploy.Class = employee.Class;
                ExistingEmploy.Gender = employee.Gender;
                ExistingEmploy.Salary = employee.Salary;
                ExistingEmploy.Designation = employee.Designation;
                ExistingEmploy.DateOfBirth = employee.DateOfBirth;
                ExistingEmploy.Image = employee.Image;

                await _IrepEmploy.Update(ExistingEmploy);
            }
            return Ok(ExistingEmploy);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var employId = await _IrepEmploy.GetById(id);
            return Ok(employId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var EmployId = await _IrepEmploy.GetById(id);
            if (EmployId == null) return NoContent();
            await _IrepEmploy.Delete(id);
            return Ok(EmployId);
        }

    }
}
