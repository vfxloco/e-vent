﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Models
{
    public class TipoImovel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public Status Status { get; set; }
    }
}