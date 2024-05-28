using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Helpers
{
    public class ObterDependentesPontuantes : IObterDependentesPontuantes
    {
        IVerificaPessoaMenoridade _verificaPessoaMenoridadeService;
        public ObterDependentesPontuantes(IVerificaPessoaMenoridade verificaPessoaMenoridadeService)
        {
            _verificaPessoaMenoridadeService = verificaPessoaMenoridadeService;
        }
    
        public int Obter(List<Pessoa> dependentes)
        {
            var quantidade = Constantes.Zero;

            dependentes?.ForEach(pessoa =>
            {
                var menoridade = _verificaPessoaMenoridadeService.Verificar(pessoa);

                if (menoridade)
                    quantidade++;

            });

            return quantidade;
        }
    }
}
