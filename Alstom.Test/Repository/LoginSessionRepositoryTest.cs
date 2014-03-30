using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Configuration.Identity;
using Alstom.Model;
using Alstom.Repository;
using NUnit.Framework;

namespace Alstom.Test.Repository
{
    public class LoginSessionRepositoryTest : BaseRepositoryTest<LoginSession>
    {
        public IBasicRepository<Login> LoginRepository { get; set; }

        private Login CreateLogin()
        {
            Login login = new Login()
            {

                Active = true,
                Email = "jacobeo@gmail.com",
                FirstName = "Jacobo",
                LastName = "Marcos Perchin",
                UserLogin = "Jacobeo",
                Password = "Password",
                PasswordExpirationDate = DateTime.UtcNow.AddMonths(6)
            };

            return this.LoginRepository.Save(login);
        }

        [Test]
        public void AddLoginSession()
        {
            Login login = CreateLogin();

            LoginSession loginSession = new LoginSession
                                            {
                                                Login = login,
                                                SessionGuid = Guid.NewGuid()
                                            };

            LoginSession loginSessionSaved = this.BasicRepository.Save(loginSession);

            Assert.IsTrue(loginSessionSaved.Id.HasValue);
            loginSessionSaved = RefreshEntityFromDatabase(loginSession.Id.Value);
            Assert.AreEqual(loginSession.Login,loginSessionSaved.Login);
            Assert.AreEqual(loginSession.SessionGuid, loginSessionSaved.SessionGuid);
            VerifyAuditInfoAfterInsert(loginSession);

        }


        [Test]
        public void UpdateLoginSession()
        {
            Login login = CreateLogin();

            LoginSession loginSession = new LoginSession
            {
                Login = login,
                SessionGuid = Guid.NewGuid()
            };

            loginSession = this.BasicRepository.Save(loginSession);

            ThreadIdentity.SetCurrentThreadIdentity("Update Test Identity");
            loginSession.SessionGuid = Guid.NewGuid();

            DateTime beforeUpdate = DateTime.UtcNow;
            BasicRepository.Update(loginSession);
            LoginSession loginSessionUpdated = RefreshEntityFromDatabase(loginSession.Id.Value);

            Assert.AreEqual(loginSession.Login, loginSessionUpdated.Login);
            Assert.AreEqual(loginSession.SessionGuid, loginSessionUpdated.SessionGuid);

            VerifyAuditInfoAfterUpdate(loginSessionUpdated, beforeUpdate, "Update Test Identity");
        }

        [Test]
        public void DeleteLoginSession()
        {
            Login login = CreateLogin();

            LoginSession loginSession = new LoginSession
            {
                Login = login,
                SessionGuid = Guid.NewGuid()
            };

            loginSession = this.BasicRepository.Save(loginSession);

            ThreadIdentity.SetCurrentThreadIdentity("Delete Test Identity");
            DateTime beforeUpdate = DateTime.UtcNow;
            BasicRepository.Delete(loginSession);
            LoginSession loginSessionDeleted = RefreshEntityFromDatabase(loginSession.Id.Value);

            VerifyAuditInfoAfterDelete(loginSessionDeleted, beforeUpdate, "Delete Test Identity");


        }
    }
}
