using Ranqueamento.API.Dtos;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Interfaces
{
    public interface IRanqueadorFamilias
    {
        List<FamiliaDto> Ranquear(List<Familia> familias);
    }
}
