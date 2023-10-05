using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace w15_1.Models
{
    public class Utenti
    {
        public int  IdUtente { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}