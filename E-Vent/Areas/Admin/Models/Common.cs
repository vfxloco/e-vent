using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Vent.Areas.Admin.Models
{
    public class Common
    {
        public enum Status
        {
            Desativado = 0,
            Ativado = 1
        }

        public enum TipoPessoa
        {
            Física = 1,
            Jurídica = 2
        }
    }
}