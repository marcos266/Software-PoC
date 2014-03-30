using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alstom.Configuration.Identity
{
    public class CustomIdentity : System.Security.Principal.IIdentity
    {
        private string identity;

        public CustomIdentity()
        {
            this.identity = "not defined";
        }
        public CustomIdentity(string identity)
        {
            this.identity = identity;
        }

        public string Name
        {
            get { return identity; }
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}
