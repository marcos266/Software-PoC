using System;
using NHibernate.Mapping.Attributes;

namespace Alstom.Configuration.Auditory
{
    [Component]
    public class Auditable
    {
        public Auditable()
        {
            CreationUser = ModificationUser = string.Empty;
            CreationDate = ModificationDate = DateTime.Today;
        }

        [Property(NotNull = true)]
        public virtual bool IsDeleted { get; set; }

        [Property(NotNull = true)]
        public virtual DateTime CreationDate { get; set; }

        [Property(NotNull = true)]
        public virtual DateTime ModificationDate { get; set; }

        [Property(NotNull = true)]
        public virtual string CreationUser { get; set; }

        [Property(NotNull = true)]
        public virtual string ModificationUser { get; set; }
    }
}
