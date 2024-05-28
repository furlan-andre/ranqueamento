using Ranqueamento.API.Configuracao;

namespace Ranqueamento.API.Interfaces
{
    public interface IConfiguracoes
    {
        public List<ConfiguracaoRenda> ObterConfiguracaoRenda();
        public List<ConfiguracaoDependente> ObterConfiguracaoDependente();
    }
}
