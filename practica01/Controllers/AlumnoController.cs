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
        //metodo para consultar todo una tabla
        public IActionResult Get()
        {
            List<Alumno> listadoAlumno = (from e in _alumnoContext.Alumno select e).ToList();

            if (listadoAlumno.Count == 0) { return NotFound(); }
            return Ok(listadoAlumno);
        }

        //      localhost/apis/bd/metodo
        //url = localhost/api/equipos/getbyid?id=23&nombre=pwa
        [HttpGet]
        [Route("getbyid/{id}")]
        //metodo para consultar con id
        public IActionResult Get(int id) {
            Alumno? alumno = (from e in _alumnoContext.Alumno where e.cod == id select e).FirstOrDefault();
            if (alumno == null) return NotFound();
            return Ok(alumno);
        }

        [HttpGet]
        [Route("find")]

        //metodo para consultar con filtros 
        public IActionResult Buscar(String filtro)
        {
            //El contexto va a la base de datos

            //List<Alumno> alumno = (from e in _alumnoContext.Alumno where e.nombre == filtro select e).ToList();
            //uso del Like

            List<Alumno> alumno = (from e in _alumnoContext.Alumno where e.nombre.Contains(filtro) select e).ToList();

            //uso del or
            //List<Alumno> alumno = (from e in _alumnoContext.Alumno where e.nombre.Contains(filtro) || e.apellido.Contains(filtro) select e).ToList();

            //si existe algun registro
            if (alumno.Any()) return Ok(alumno);
            return NotFound();

            //if (alumno.Count() == 0) return NotFound();
            //return Ok(alumno);
        }

        //se usa comunmente con post por que ya viene habilitado
        //put y otros verbos deben codificarse
        [HttpPost]
        [Route("add")]

        //filosofia de sully
        //metodo para ingresar en la base de datos
        public IActionResult crear([FromBody] Alumno pAlumno)
        {
            try
            {
                _alumnoContext.Alumno.Add(pAlumno);
                //es como el execute query
                _alumnoContext.SaveChanges();

                //se obtendran los datosd de la base de datos
                //se puede solo obtener solo un elemento del objeto para que el tamaño del request sea menor
                return Ok(pAlumno);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //metodo para modificar
        [HttpPut]
        [Route("Actulizar/{id}")]
        public IActionResult actulizarAlumno(int id, [FromBody] Alumno pAlumno)
        {
            Alumno? alumno = (from e in _alumnoContext.Alumno where e.cod == id select e).FirstOrDefault();

            if (alumno == null)
                return NotFound();
            

            alumno.nombre = pAlumno.nombre;
            alumno.apellido = pAlumno.apellido;

            _alumnoContext.Entry(alumno).State = EntityState.Modified;
            _alumnoContext.SaveChanges();

            return Ok(alumno.nombre);
        }

        // no se recomienda borrar sino que se recomienda cambiar el estado
        //metodo para eliminar en la base de datos
        [HttpDelete]
        [Route("Eliminar")]
        public IActionResult eliminar(int id)
        {
            Alumno? alumno = (from e in _alumnoContext.Alumno where e.cod == id select e).FirstOrDefault();

            if (alumno == null)
                return NotFound();
                

            _alumnoContext.Alumno.Attach(alumno);
            _alumnoContext.Alumno.Remove(alumno);
            _alumnoContext.SaveChanges();

            return Ok();
        }
    }
}
