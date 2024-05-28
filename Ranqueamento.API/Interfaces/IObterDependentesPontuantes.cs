using Ranqueamento.API.Model;

namespace Ranqueamento.API.Interfaces
{
    public interface IObterDependentesPontuantes
    {
        public int Obter(List<Pessoa> dependentes);
    }
}
