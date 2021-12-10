using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels.User;
using Agenda.MVC.ViewModels.User.ValidatorAttributes;

namespace Agenda.MVC.ViewModels
{
    public class UserFormViewModel
    {

        public string Id { get; set; }

        [DisplayName("Role")]
        public string RoleId { get; set; }

        [NameNotNullOrWhiteSpaceValidator]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [UsernameMinimumLenghtValidator(16, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "É necessário um Email")]
        [EmailAddress(ErrorMessage = "O email tem que ser válido")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        [PasswordMinimumLenghtValidator(30, MinimumLength = 6)]
        [PasswordWithAtLeastOneNumberValidator]
        public string Password { get; set; }

    }
}
