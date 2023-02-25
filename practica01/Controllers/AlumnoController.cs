using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica01.Models;
using Microsoft.EntityFrameworkCore;

namespace practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly alumnoContext _alumnoContext;

        public AlumnoController(alumnoContext alumnoContext)
        {
            _alumnoContext = alumnoContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Alumno> listadoAlumno = (from e in _alumnoContext.Alumno select e).ToList();

            if (listadoAlumno.Count == 0) { return NotFound(); }
            return Ok(listadoAlumno);
        }
    }
}
