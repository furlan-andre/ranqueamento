using Microsoft.EntityFrameworkCore;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;
using Ranqueamento.API.Models;

namespace Ranqueamento.API.DataBase
{
    public class FamiliaRepositorio: IFamiliaRepositorio
    {
        private readonly ProjetoContext _context;

        public FamiliaRepositorio(ProjetoContext context)
        {
            _context = context;
        }

        public async Task<List<Familia>> ObterTodosAsync()
        {
            var familias = new List<Familia>();

            var retorno = await _context.Familia
                                        .Include(f => f.Dependentes)
                                        .ToListAsync();     
            
            if (retorno.Any())
                familias.AddRange(retorno);

            
            return retorno;
        }
    }
}
