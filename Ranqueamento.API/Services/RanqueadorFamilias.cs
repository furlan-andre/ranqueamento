using Ranqueamento.API.Dtos;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Services
{
    public class RanqueadorFamilias: IRanqueadorFamilias
    {
        private readonly ICalcularPontos _calcularPontos;

        public RanqueadorFamilias(ICalcularPontos calcularPontos)
        {
            _calcularPontos = calcularPontos;
        }

        public List<FamiliaDto> Ranquear(List<Familia> familias)
        {
            var retorno = new List<FamiliaDto>();

            familias.ForEach(familia =>
            {
                var pontuacao = _calcularPontos.Calcular(familia);

                var dto = new FamiliaDto(familia);
                dto.Pontuar(pontuacao);

                retorno.Add(dto);   
            });

            var ordenado = retorno.OrderByDescending(x => x.Pontuacao).ToList();
            return ordenado;
        }
    }
}
