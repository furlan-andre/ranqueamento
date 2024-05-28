using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Helpers
{
    public class VerificaPessoaMenoridade : IVerificaPessoaMenoridade
    {
        public bool Verificar(Pessoa pessoa)
        {
            if (pessoa.Idade < Constantes.InicioMaioridade)
                return true;

            return false;
        }
    }
}
