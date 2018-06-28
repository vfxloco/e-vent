using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public int Cidade_Id { get; set; }
    }

    public class Publicacao
    {
        public int Id { get; set; }
        public int Pessoa_Id { get; set; }
        public int Tipo_Imovel_Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Publicacao { get; set; }
        public Status Status { get; set; }
        public string Imagem { get; set; }
    }

    public class PublicacaoView
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public int Cidade_Id { get; set; }
        public int Pessoa_Id { get; set; }
        public int Tipo_Imovel_Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Publicacao { get; set; }
        public string Imagem { get; set; }
        public Status Status { get; set; }
    }

    public class Event
    {
        public int EventId { get; set; }
        public int Publicacao_Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}