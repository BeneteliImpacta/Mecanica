using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Models
{
    public class Peca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
