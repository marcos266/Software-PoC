using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Alstom.Model
{
    [NHibernate.Mapping.Attributes.Class(Where = "IsDeleted = 0")]
    public class Login : BaseEntity
    {
        public Login()
            : base()
        {
            Active = true;
            LoginSessions = new List<LoginSession>();
        }

        [Property(NotNull = true)]
        public virtual bool Active { get; set; }
        
        [Property(NotNull = true)]
        public virtual string FirstName { get; set; }
        
        [Property(NotNull = true)]
        public virtual string LastName { get; set; }
        
        [Property(NotNull = true)]
        public virtual string Email { get; set; }
        
        [Property(NotNull = true)]
        public virtual string UserLogin { get; set; }

        [Property(NotNull = true)]
        public virtual string Password { get; set; }

        [Property(NotNull = true)]
        public virtual DateTime PasswordExpirationDate { get; set; }

        [Set(0, Name = "LoginSessions", Inverse = true)]
        [Key(1, Column = "LoginId")]
        [OneToMany(2, ClassType = typeof(LoginSession))]
        public virtual ICollection<LoginSession> LoginSessions { get; set; }
    }
}
