using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase.CSV.Application.Models
{
    public  class AuthResponse
    {
        public string Token { get; set; }  // Token Değeri
        public DateTime Expiration { get; set; } // Token geçerlilik süresi
    }
}
