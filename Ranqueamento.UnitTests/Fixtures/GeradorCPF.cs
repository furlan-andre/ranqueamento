using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranqueamento.UnitTests.Fixtures
{
    public static class GeradorCPF
    {
        public static string Obter()
        {
            var random = new Random();

            var primeiroGrupo = random.Next(1, 999).ToString();
            var segundoGrupo = random.Next(1, 999).ToString();
            var terceiroGrupo = random.Next(1, 999).ToString();
            var verificador = random.Next(0, 99).ToString();

            return 
                $"{primeiroGrupo.PadLeft(3, '0')}" +
                $".{segundoGrupo.PadLeft(3, '0')}" +
                $".{terceiroGrupo.PadLeft(3, '0')}" +
                $"-{verificador.PadLeft(2, '0')}";
        }
    }
}
