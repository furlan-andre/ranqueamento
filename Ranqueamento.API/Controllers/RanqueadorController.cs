using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ranqueamento.API.DataBase;
using Ranqueamento.API.Helpers;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;
using Ranqueamento.API.Models;

namespace Ranqueamento.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RanqueadorController : ControllerBase
    {
        private readonly IFamiliaRepositorio _familiaRepositorio;
        private readonly IRanqueadorFamilias _ranqueadorFamilias ;

        public RanqueadorController(IFamiliaRepositorio familiaRepositorio, IRanqueadorFamilias ranqueadorFamilias)
        {
            _familiaRepositorio = familiaRepositorio;
            _ranqueadorFamilias = ranqueadorFamilias;
        }

        [HttpGet]
        public async Task<IActionResult> ObterListaRanqueadaFamilias()
        {
            try
            {
                var familias = await _familiaRepositorio.ObterTodosAsync();

                var resultado = _ranqueadorFamilias.Ranquear(familias);

                foreach (var item in resultado)
                {
                    Console.WriteLine(item);
                }

                return Ok(resultado);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
