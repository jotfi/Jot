using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public class Password : Transaction
    {
        public string PasswordHash { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }

        public override string CreateTable()
        {
            throw new NotImplementedException();
        }
    }
}
