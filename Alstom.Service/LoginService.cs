using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Model;
using Alstom.Repository;
using Spring.Transaction.Interceptor;

namespace Alstom.Service
{
    public class LoginService: BasicService<Login>, ILoginService
    {
        public IBasicRepository<LoginSession> LoginSessionRepository { get; set; }

        [Transaction(ReadOnly = false)]
        public Guid? LogIn(string user, string password)
        {
            Guid? result = null;
            Login login = this.BasicRepository.FindAll().FirstOrDefault(x => x.UserLogin.Equals(user, StringComparison.OrdinalIgnoreCase) &&
                                                                             x.Password.Equals(password));

            if (login != null)
            {
                result = Guid.NewGuid();
                LoginSessionRepository.Save(new LoginSession() {Login = login, SessionGuid = result.Value});
            }

            return result;
        }

        [Transaction(ReadOnly = false)]
        public void LogOut(Guid session)
        {
            LoginSession loginSession = LoginSessionRepository.FindAll().FirstOrDefault(x => x.SessionGuid.Equals(session));            

            if (loginSession != null)
            {
                LoginSessionRepository.Delete(loginSession);
            }
        }

    }
}
