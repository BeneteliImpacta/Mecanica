using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MecanicaBeneteli.ViewModel
{
    public class UsuarioViewModel
    {
        [DisplayName("Nome do Usuário")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Nome { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string? CPF { get; set; }

        [DisplayName("Usuário")]
        [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
        public string? IdUsuario { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string? Senha { get; set; }
    }
}
