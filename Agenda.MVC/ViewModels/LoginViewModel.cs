using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "O campo Username é obrigatório")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Username inválido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Senha inválida")]
        [DisplayName("Senha")]
        public string Password { get; set; }

    }
}
