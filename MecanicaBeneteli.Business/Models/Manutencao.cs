using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Models
{
    public class Manutencao
    {
        public string NomeUsuario { get; set; }
        public string CarroUsado { get; set; }
        public string Valor { get; set; }
        public string Placa { get; set; }
        public Peca Peca { get; set; }
        public List<Peca> Peca1 { get; set;}
    }
}
