using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Alstom.Configuration.Auditory;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace Alstom.Configuration.EventListener
{
    public class InsertEventListener : NHibernate.Event.IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent @event)
        {
            if (@event.Entity is IAuditable)
            {
                DateTime now = DateTime.UtcNow;
                IAuditable audit = @event.Entity as IAuditable;
                audit.AuditInfo.IsDeleted = false;
                audit.AuditInfo.CreationDate = now;
                audit.AuditInfo.ModificationDate = now;
                audit.AuditInfo.CreationUser = Thread.CurrentPrincipal.Identity.Name;
                audit.AuditInfo.ModificationUser = Thread.CurrentPrincipal.Identity.Name;

                Auditable auditable = audit.AuditInfo;
                Set(@event.Persister, @event.State, "AuditInfo", auditable);

                return false;
            }
            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}
