using Ranqueamento.API.Model;

namespace Ranqueamento.API.Interfaces
{
    public interface ICalcularPontosDependente
    {
        int Calcular(List<Pessoa> dependentes);
    }
}
