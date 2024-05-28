using Ranqueamento.API.Helpers;
using Ranqueamento.API.Model;

namespace Ranqueamento.UnitTests.Fixtures
{
    public sealed class PessoaBuilder
    {
        private Random _random;
        private Pessoa _pessoa;
        private int _inicioMaioridade;

        public PessoaBuilder()
        {
            _random = new Random();
            _pessoa = new Pessoa();
            _inicioMaioridade = Constantes.InicioMaioridade;
        }

        public static PessoaBuilder Novo()
        {
            return new PessoaBuilder();
        }

        public Pessoa Obter()
        {
            return _pessoa;
        }

        public PessoaBuilder ComNome(bool gerarSobrenome = true)
        {
            _pessoa.Nome = GeradorNome.Obter(gerarSobrenome);
            return this;
        }

        public PessoaBuilder ComMaioridade()
        {
            var limiteSuperior = _inicioMaioridade * _random.Next(1, 6);
            _pessoa.Idade = _random.Next(_inicioMaioridade, limiteSuperior);
            return this;
        }

        public PessoaBuilder ComMenoridade()
        {
            var limiteSuperior = _inicioMaioridade - Constantes.Um;
            _pessoa.Idade = _random.Next(0, limiteSuperior);
            return this;
        }
    }
}
