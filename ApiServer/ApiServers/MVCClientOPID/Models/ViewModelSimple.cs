﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MVCClientOPID.Models
{
    public class ViewModelSimple
    {
        public string UserName { get; set; }
        public string TelNum { get; set; }
        public string IdToken { get; set; }

        public string AccessToken { get; set; }
        public string ResponseMessage { get; set; }
    }
}
