using DIO.Series.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DIO.Series.Web.Controllers
{
    [Route("[controller]")]
    public class SerieController : Controller
    {
        private readonly IRepositorio<Serie> _repositorio;

        public SerieController(IRepositorio<Serie> repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("")]
        public IActionResult Lista()
        {
            return Ok(_repositorio.Lista().Select(s => new SerieModel(s)));
        }

        [HttpGet("{id}")]
        public IActionResult Consulta(int id)
        {
            Serie serie = _repositorio.Lista().FirstOrDefault(s => s.retornaId() == id);

            SerieModel model = new SerieModel(serie);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Atualiza(int id, [FromBody] SerieModel model)
        {
            _repositorio.Atualiza(id, model.ToSerie());

            return NoContent();
        }

        [HttpPost("")]
        public IActionResult Insere([FromBody] SerieModel model)
        {
            model.Id = _repositorio.ProximoId();
            
            Serie serie = model.ToSerie();

            _repositorio.Insere(serie);

            return Created("", serie);
        }

        [HttpDelete("{id}")] //[HttpDelete()] é o método da minha Action
        public IActionResult Exclui(int id)
        {
            _repositorio.Exclui(id);

            return NoContent();
        }

        /*
         * Servidor  https://localhost:
         * Porta    4322/
         * Controller serie/
         * Action ID
        */
    }
}
