using Ranqueamento.API.Model;

namespace Ranqueamento.API.Interfaces
{
    public interface IVerificaPessoaMenoridade
    {
        public bool Verificar(Pessoa pessoa);
    }
}
