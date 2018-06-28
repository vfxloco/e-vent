using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Models.Pessoa
{
    public class Pessoa_Fisica : Pessoa
    {
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime Data_Nascimento { get; set; }
    }

    public class Pessoa_FisicaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Website { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public TipoPessoa Tipo_Pessoa { get; set; }
        public int Status { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime Data_Nascimento { get; set; }
    }
}