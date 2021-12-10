using System.Collections.Generic;
using System.Linq;
using Agenda.Application.Validators.ContactTelephone;
using Agenda.Application.ViewModels.Contact.Base;
using Agenda.Application.ViewModels.ContactTelephone;
using FluentValidation;

namespace Agenda.Application.Validators.Contact.Base
{
    public class ContactRequestBaseValidator<T> : AbstractValidator<T> where T : ContactRequestBase
    {

        public ContactRequestBaseValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                    .WithMessage("O nome do contato não pode ser vazio")
                .MaximumLength(200)
                    .WithMessage("O nome do contato deve ter no máximo 200 caracteres");

            RuleFor(c => c.Telephones)
                .Must(AllTelephonesUnique)
                    .WithMessage("Lista de telefones com telefones duplicados");

            RuleForEach(c => c.Telephones)
                .SetValidator(new ContactTelephoneRequestValidator());
        }

        private bool AllTelephonesUnique(List<ContactTelephoneRequest> contactTelephones)
        {
            return contactTelephones.Select(t => t.TelephoneNumber).Distinct().Count() == contactTelephones.Count;
        }

    }
}
