using Ranqueamento.API.Interfaces;

namespace Ranqueamento.API.Configuracao
{
    public sealed class Configuracoes : IConfiguracoes
    {
        private List<ConfiguracaoRenda> configuracaoRenda { get; }
        private List<ConfiguracaoDependente> configuracaoDependente { get; }

        private ConfiguracaoRenda renda1 = new ConfiguracaoRenda { Pontuacao = 5, RendaMaxima = 900 };
        private ConfiguracaoRenda renda2 = new ConfiguracaoRenda { Pontuacao = 3, RendaMaxima = 1500 };

        private ConfiguracaoDependente dependente1 = new ConfiguracaoDependente { Pontuacao = 3, DependenteMinimo = 3 };
        private ConfiguracaoDependente dependente2 = new ConfiguracaoDependente { Pontuacao = 2, DependenteMinimo = 1 };

        public Configuracoes()
        {
            configuracaoRenda = new List<ConfiguracaoRenda>(); 
            configuracaoRenda.Add(renda1);
            configuracaoRenda.Add(renda2);

            configuracaoDependente = new List<ConfiguracaoDependente>();
            configuracaoDependente.Add(dependente1);
            configuracaoDependente.Add(dependente2);
        }

        public List<ConfiguracaoRenda> ObterConfiguracaoRenda()
        {
            return this.configuracaoRenda;
        }

        public List<ConfiguracaoDependente> ObterConfiguracaoDependente()
        {
            return this.configuracaoDependente;
        }
    }
}
