using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Configuration.Auditory;

namespace Alstom.Configuration.Auditory
{
    public interface IAuditable
    {
        Auditable AuditInfo { get; set; }
    }
}
