using Ranqueamento.API.Helpers;
using Ranqueamento.API.Model;

namespace Ranqueamento.UnitTests.Fixtures
{
    public sealed class DependentesBuilder
    {
        private List<Pessoa> _dependentes;

        public DependentesBuilder()
        {
            _dependentes = new List<Pessoa>();
        }

        public static DependentesBuilder Novo()
        {
            return new DependentesBuilder();
        }

        public List<Pessoa> Obter()
        {
            return _dependentes;
        }

        public DependentesBuilder ComMenoridade(int quantidade = 0)
        {
            var dependentes = MontarDependentes(quantidade);

            if(dependentes.Count() > Constantes.Zero)
                _dependentes.AddRange(dependentes);

            return this;
        }

        public DependentesBuilder ComMaioridade(int quantidade = 0)
        {
            var dependentes = MontarDependentes(quantidade, false);
            
            if (dependentes.Count() > Constantes.Zero)
                _dependentes.AddRange(dependentes);

            return this;
        }

        private List<Pessoa> MontarDependentes(int quantidade = 0, bool menoridade = true)
        {
            var dependentes = new List<Pessoa>();

            for (int i = Constantes.Zero; i < quantidade; i++)
            {
                var builder = PessoaBuilder.Novo()
                                           .ComNome(false);

                builder = menoridade
                    ? builder.ComMenoridade()
                    : builder.ComMaioridade();

                var pessoa = builder.Obter();

                dependentes.Add(pessoa);
            }

            return dependentes;
        }
    }
}
