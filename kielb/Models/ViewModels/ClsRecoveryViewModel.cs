﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kielb.Models.ViewModels
{
    public class ClsRecoveryViewModel
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }
    }
}