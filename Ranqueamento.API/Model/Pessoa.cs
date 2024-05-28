using System.Text.Json.Serialization;

namespace Ranqueamento.API.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        public int FamiliaId { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        [JsonIgnore]
        public virtual Familia Familia { get; set; }
    }
}
