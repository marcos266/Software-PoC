using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Configuration.Auditory;
using Alstom.Configuration.Identity;
using Alstom.Model;
using NUnit.Framework;

namespace Alstom.Test.Repository
{
    public class LoginRepositoryTest: BaseRepositoryTest<Login>
    {
        [Test]
        public void AddLogin()
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

            Login loginSaved= this.BasicRepository.Save(login);
            Assert.IsTrue(loginSaved.Id.HasValue);
            loginSaved = RefreshEntityFromDatabase(loginSaved.Id.Value);
            Assert.AreEqual(login.Active, loginSaved.Active);
            Assert.AreEqual(login.Email, loginSaved.Email);
            Assert.AreEqual(login.FirstName, loginSaved.FirstName);
            Assert.AreEqual(login.LastName, loginSaved.LastName);
            Assert.AreEqual(login.UserLogin, loginSaved.UserLogin);
            Assert.AreEqual(login.Password, loginSaved.Password);
            Assert.AreEqual(login.PasswordExpirationDate, loginSaved.PasswordExpirationDate);
            VerifyAuditInfoAfterInsert(loginSaved);



        }


        [Test]
        public void UpdateLogin()
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

            login = this.BasicRepository.Save(login);

            ThreadIdentity.SetCurrentThreadIdentity("Update Test Identity");
            login.Email = "jacobo_marcos@homail.com";
            login.Password = "PasswordUpdate";
            login.PasswordExpirationDate = DateTime.UtcNow.AddMonths(12);

            DateTime beforeUpdate = DateTime.UtcNow;
            BasicRepository.Update(login);
            Login loginUpdated = RefreshEntityFromDatabase(login.Id.Value);

          
            Assert.AreEqual(login.Active, loginUpdated.Active);
            Assert.AreEqual(login.Email, loginUpdated.Email);
            Assert.AreEqual(login.FirstName, loginUpdated.FirstName);
            Assert.AreEqual(login.LastName, loginUpdated.LastName);
            Assert.AreEqual(login.UserLogin, loginUpdated.UserLogin);
            Assert.AreEqual(login.Password, loginUpdated.Password);
            Assert.AreEqual(login.PasswordExpirationDate, loginUpdated.PasswordExpirationDate);
            VerifyAuditInfoAfterUpdate(loginUpdated, beforeUpdate, "Update Test Identity");
        }

        [Test]
        public void DeleteLogin()
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

            login = this.BasicRepository.Save(login);
           
            ThreadIdentity.SetCurrentThreadIdentity("Delete Test Identity");
            DateTime beforeUpdate = DateTime.UtcNow;
            BasicRepository.Delete(login);
            Login loginDeleted = RefreshEntityFromDatabase(login.Id.Value);

            VerifyAuditInfoAfterDelete(loginDeleted, beforeUpdate, "Delete Test Identity");

         
        }
    }
}
