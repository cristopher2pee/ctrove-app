﻿using CTrove.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Entity
{
    public class RolesPages
    {
        public Guid Id { get; set; }
        public Guid? RolesId {  get; set; }
        public Pages Pages { get; set; }
        public bool Status { get; set; }
    }
}
