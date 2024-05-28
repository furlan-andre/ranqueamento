using Ranqueamento.API.Helpers;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Services
{

    public class CalcularPontosDependente : ICalcularPontosDependente
    {
        private readonly IObterDependentesPontuantes _obterDependentesPontuantes;
        private readonly IConfiguracoes _configuracoes;

        public CalcularPontosDependente
        (
            IObterDependentesPontuantes obterDependentesPontuantes, 
            IConfiguracoes configuracoes
        )
        {
            _obterDependentesPontuantes = obterDependentesPontuantes;
            _configuracoes = configuracoes;
        }

        public int Calcular(List<Pessoa> dependentes)
        {
            var dependentesComMenoridade = _obterDependentesPontuantes.Obter(dependentes);
            var configuracaoDependentes = _configuracoes.ObterConfiguracaoDependente();

            foreach (var item in configuracaoDependentes)
            {
                if (dependentesComMenoridade >= item.DependenteMinimo)
                    return item.Pontuacao;
            }

            return Constantes.Zero;
        }
    }
}
