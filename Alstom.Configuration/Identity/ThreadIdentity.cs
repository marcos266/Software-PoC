using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Alstom.Configuration.Identity
{
    public class ThreadIdentity
    {
        public static void SetCurrentThreadIdentity(string identityName)
        {
            //Set the CustomIdentity object on current threads for given username
            CustomIdentity customIdentity = new CustomIdentity(identityName);
            GenericPrincipal threadCurrentPrincipal = new GenericPrincipal(customIdentity, new string[] { "CustomUser" });
            System.Threading.Thread.CurrentPrincipal = threadCurrentPrincipal;

        }
    }
}
