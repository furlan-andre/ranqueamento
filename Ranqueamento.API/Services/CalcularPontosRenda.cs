using Ranqueamento.API.Configuracao;
using Ranqueamento.API.Helpers;
using Ranqueamento.API.Interfaces;

namespace Ranqueamento.API.Services
{
    public class CalcularPontosRenda: ICalcularPontosRenda
    {
        private readonly IConfiguracoes _configuracoes;

        public CalcularPontosRenda(IConfiguracoes configuracoes)
        {
            _configuracoes = configuracoes;
        }
                                                                                
        public int Calcular(double renda)
        {
            var configuracaoRenda = _configuracoes.ObterConfiguracaoRenda();

            foreach (var item in configuracaoRenda)
            {
                if (renda <= item.RendaMaxima)
                    return item.Pontuacao;
            }

            return Constantes.Zero;
        }
    }
}
