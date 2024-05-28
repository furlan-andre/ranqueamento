using Ranqueamento.API.Model;

namespace Ranqueamento.API.Interfaces
{
    public interface IFamiliaRepositorio
    {
        Task<List<Familia>> ObterTodosAsync();        
    }
}
