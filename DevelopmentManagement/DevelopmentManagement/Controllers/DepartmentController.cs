using DevelopmentManagement.BL;
using DevelopmentManagement.BL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager departmentManager;

        public DepartmentController(IDepartmentManager departmentManager)
        {
            this.departmentManager = departmentManager;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DepartmentReadDto> getDepartmentById(int id)
        {
            DepartmentReadDto? depatment = departmentManager.getById(id);
            if (depatment == null)
            {
                return NotFound();  
            }

            return depatment;
        }
    }
}
