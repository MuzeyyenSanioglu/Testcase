﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase.User.Domain.Entites
{
    public class AccessToken
    {
        public string Token { get; set; }  // Token Değeri
        public DateTime Expiration { get; set; } // Token geçerlilik süresi
    }
}
