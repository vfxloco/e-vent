using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Models.Pessoa
{
    public class Telefone
    {
        public string Numero { get; set; }
        public bool Whats { get; set; }
    }

    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Website { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public TipoPessoa Tipo_Pessoa { get; set; }
        public int Status { get; set; }
    }

    public class PessoaViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Website { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public int Tipo_Pessoa { get; set; }
        public int Status { get; set; }

        //------ Fisica
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime Data_Nascimento { get; set; }

        //------ Juridica
        public string Insc_Estadual { get; set; }
        public string CNPJ { get; set; }
        public DateTime Data_Fundacao { get; set; }
    }

    public class PessoaViewLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class PessoaSession
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoPessoa Tipo_Pessoa { get; set; }
        public Status Status { get; set; }
    }
}