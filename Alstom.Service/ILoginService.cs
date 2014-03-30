using System;
using Alstom.Model;
using Alstom.Repository;
using Spring.Transaction.Interceptor;

namespace Alstom.Service
{
    public interface ILoginService
    {
        IBasicRepository<LoginSession> LoginSessionRepository { get; set; }

        [Transaction(ReadOnly = false)]
        Guid? LogIn(string user, string password);

        [Transaction(ReadOnly = false)]
        void LogOut(Guid session);
    }
}