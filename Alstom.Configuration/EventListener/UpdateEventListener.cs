using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Alstom.Configuration.Auditory;
using NHibernate.Event;

namespace Alstom.Configuration.EventListener
{
    public class UpdateEventListener : NHibernate.Event.IPreUpdateEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            if (@event.Entity is IAuditable)
            {
                DateTime now = DateTime.UtcNow;
                IAuditable audit = @event.Entity as IAuditable;
                audit.AuditInfo.ModificationUser = Thread.CurrentPrincipal.Identity.Name;
                audit.AuditInfo.ModificationDate = now;

                return false;
            }
            return false;
        }
    }
}
