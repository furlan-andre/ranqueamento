using Ranqueamento.API.Helpers;
using Ranqueamento.API.Model;

namespace Ranqueamento.UnitTests.Fixtures
{
    public sealed class FamiliaBuilder
    {
        private Familia _familia;

        public FamiliaBuilder()
        {
            _familia = new Familia();            
        }

        public static FamiliaBuilder Novo()
        {
            return new FamiliaBuilder();
        }

        public Familia Obter()
        {
            return _familia;
        }

        public List<Familia> ObterLista(int quantidade = Constantes.Zero)
        {
            var random = new Random();            
            var familias = new List<Familia>();

            while (quantidade > Constantes.Zero)
            {
                var menoridades = random.Next(Constantes.Zero, Constantes.Quatro);
                var maioridades = random.Next(Constantes.Zero, Constantes.Quatro);
                var renda = (double)random.Next(Constantes.Zero, Constantes.MilQuinhentosUm);

                var familia = FamiliaBuilder.Novo()
                                            .Completa(renda, menoridades, maioridades)
                                            .Obter();
                familias.Add(familia);
                quantidade--;
            }

            return familias;
        }

        public FamiliaBuilder Completa(double rendaTotal, int dependentesMenoridade, int dependentesMaioridade)
        {
            this.ComRendaTotal(rendaTotal)
                .ComNome()
                .ComConjuge()
                .ComCpf()
                .ComDependenteMaioridade(dependentesMaioridade)
                .ComDependenteMenoridade(dependentesMenoridade);

            return this;
        }

        public FamiliaBuilder ComNome(bool gerarSobrenome = true)
        {
            _familia.Nome = GeradorNome.Obter(gerarSobrenome);
            return this;
        }

        public FamiliaBuilder ComCpf()
        {
            _familia.Cpf = GeradorCPF.Obter();
            return this;
        }

        public FamiliaBuilder ComConjuge()
        {
            _familia.Conjuge = GeradorNome.Obter();
            return this;
        }

        public FamiliaBuilder ComRendaTotal(double rendaTotal = Constantes.ZeroDouble)
        {
            if (rendaTotal != Constantes.ZeroDouble)
                _familia.RendaTotal = rendaTotal;

            return this;
        }

        public FamiliaBuilder ComDependenteMenoridade(int quantidade = Constantes.Zero)
        {
            var dependentes = DependentesBuilder.Novo()
                                                .ComMenoridade(quantidade)
                                                .Obter();

            if (dependentes.Count() > Constantes.Zero)
                _familia.Dependentes.AddRange(dependentes);

            return this;
        }

        public FamiliaBuilder ComDependenteMaioridade(int quantidade = Constantes.Zero)
        {
            var dependentes = DependentesBuilder.Novo()
                                                .ComMaioridade(quantidade)
                                                .Obter();

            if (dependentes.Count() > Constantes.Zero)
                _familia.Dependentes.AddRange(dependentes);

            return this;
        }
    }
}
