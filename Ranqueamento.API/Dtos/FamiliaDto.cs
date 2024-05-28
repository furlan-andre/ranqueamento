using Ranqueamento.API.Helpers;
using Ranqueamento.API.Model;
using System.Globalization;
using System.Text;

namespace Ranqueamento.API.Dtos
{
    public class FamiliaDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Conjuge { get; set; }
        public double RendaTotal { get; set; } = Constantes.ZeroDouble;
        public virtual List<PessoaDto> Dependentes { get; set; } = new List<PessoaDto>();
        public int? Pontuacao { get; private set; }

        public FamiliaDto(){ }

        public FamiliaDto(Familia familia)
        {
            Id = familia.Id;
            RendaTotal = familia.RendaTotal;
            Nome = familia.Nome;
            Cpf = familia.Cpf;
            Conjuge = familia.Conjuge;
            Dependentes =
                familia.Dependentes.Select(p => new PessoaDto
                {
                    Id = p.Id,
                    FamiliaId = p.FamiliaId,
                    Nome = p.Nome,
                    Idade = p.Idade
                }).ToList();
        }

        public void Pontuar(int pontos)
        {
            if (Pontuacao == null)
            {
                Pontuacao = pontos;
            }
        }

        private string ObterCpfFormatado()
        {
            var builder = new StringBuilder();
            builder.Append(Cpf.Substring(0, 3)).Append('.')
                   .Append(Cpf.Substring(3, 3)).Append('.')
                   .Append(Cpf.Substring(6, 3)).Append('-')
                   .Append(Cpf.Substring(9, 2));

            return builder.ToString();
        }

        public override string ToString()
        {
            CultureInfo culture = new CultureInfo("pt-BR");

            var dependentes = string.Empty;
            foreach (var item in Dependentes)
            {
                if (Dependentes.Last() != item && Dependentes.First() != item)
                    dependentes += ", ";

                dependentes += item.Nome + $"({item.Idade})";

            }

            return $"[{Pontuacao}] {Nome} ({ObterCpfFormatado()}) & {Conjuge} ({dependentes}) - {RendaTotal.ToString("C", culture)}";
        }
    }
}
