using Ranqueamento.API.Configuracao;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Services
{
    public class CalcularPontos: ICalcularPontos
    {
        private readonly ICalcularPontosDependente _calcularPontoDependente;
        private readonly ICalcularPontosRenda _calcularPontoRenda;

        public CalcularPontos(ICalcularPontosDependente calcularPontoDependente, ICalcularPontosRenda calcularPontoRenda)
        {
            _calcularPontoDependente = calcularPontoDependente;
            _calcularPontoRenda = calcularPontoRenda;
        }

        public int Calcular(Familia familia)
        {
            var pontuacao = new List<int>();

            pontuacao.Add(_calcularPontoRenda.Calcular(familia.RendaTotal));
            pontuacao.Add(_calcularPontoDependente.Calcular(familia.Dependentes));
            
            return pontuacao.Sum(x => x);                    
        }
    }
}
