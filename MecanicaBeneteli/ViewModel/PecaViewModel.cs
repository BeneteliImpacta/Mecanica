using System.ComponentModel;

namespace MecanicaBeneteli.ViewModel
{
    public class PecaViewModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Marca")]
        public string? Marca { get; set; }

        [DisplayName("Modelo")]
        public string? Modelo { get; set; }

        [DisplayName("Ano")]
        public int Ano { get; set; }

        [DisplayName("Preco")]
        public decimal Preco { get; set; }

        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }
    }
}
