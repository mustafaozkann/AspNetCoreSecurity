﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.Web.Models
{
    public partial class Products
    {
        [NotMapped]
        public string EncryptedId { get; set; }
    }
}
