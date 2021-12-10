using System;
using System.ComponentModel;

namespace Agenda.MVC.ViewModels
{
    public class PhonebookSearchViewModel
    {

        [DisplayName("ID")]
        public string IdContato { get; set; }

        [DisplayName("Nome")]
        public string NomeContato { get; set; }

        [DisplayName("DDD")]
        public string Ddd { get; set; }

        [DisplayName("Número telefone")]
        public string NumeroTelefone { get; set; }

        public int Page { get; set; }
        public int HowManyPages { get; private set; }

        public void CountPages(int take, int total)
        {
            HowManyPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(total) / Convert.ToDecimal(take)));
        }

    }
}
