using Ranqueamento.API.Helpers;

namespace Ranqueamento.API.Model
{
    public class Familia
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Conjuge { get; set; }
        public double RendaTotal { get; set; } = Constantes.ZeroDouble;
        public virtual List<Pessoa> Dependentes { get; set; } = new List<Pessoa>();
    
    }
}
