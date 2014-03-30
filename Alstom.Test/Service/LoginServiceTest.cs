using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Model;
using Alstom.Service;
using NUnit.Framework;

namespace Alstom.Test.Service
{
   
    public class LoginServiceTest : BaseServiceTest<Login>
    {

        private ILoginService LoginService { get { return (ILoginService) this.BasicService; } }
        public IBasicService<LoginSession> LoginSessionBasicService { get; set; }
         

       [Test]
       public void CreateDeleteTest()
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

           
           login= this.BasicService.NewInstance(login);
           int id = login.Id.Value;

           this.BasicService.DeleteInstance(login);

           Login result = BasicService.GetInstance(id);
           Assert.IsNull(result);

       }


       [Test]
       public void LogInOKTest()
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


           login = this.BasicService.NewInstance(login);

           Guid? session = LoginService.LogIn("jacobeo", "Password");

           Assert.IsNotNull(session);
           LoginSession sessionRecovered = LoginSessionBasicService.GetAll().FirstOrDefault(x => x.SessionGuid.Equals(session.Value));
           Assert.IsNotNull(sessionRecovered);

           Assert.AreEqual(login.Id,sessionRecovered.Login.Id);


       }

       [Test]
       public void LogInFailedTest()
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


           login = this.BasicService.NewInstance(login);

           Guid? session = LoginService.LogIn("jacobeo", "password");

           Assert.IsNull(session);
       }

       [Test]
       public void LogInLogOutTest()
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


           login = this.BasicService.NewInstance(login);

           Guid? session = LoginService.LogIn("jacobeo", "Password");

           Assert.IsNotNull(session);
           LoginSession sessionRecovered = LoginSessionBasicService.GetAll().FirstOrDefault(x => x.SessionGuid.Equals(session.Value));
           Assert.IsNotNull(sessionRecovered);

           Assert.AreEqual(login.Id, sessionRecovered.Login.Id);

           LoginService.LogOut(session.Value);
           LoginSession sessionRecoveredAfterLogout = LoginSessionBasicService.GetAll().FirstOrDefault(x => x.SessionGuid.Equals(session.Value));
           Assert.IsNull(sessionRecoveredAfterLogout);


       }
    }
}
