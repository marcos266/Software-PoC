using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Configuration.Identity;
using Alstom.Configuration.SchemaGeneration;
using Alstom.Model;
using Alstom.Repository;
using NHibernate;
using NUnit.Framework;
using Spring.Testing.NUnit;
using log4net.Config;


namespace Alstom.Test.Repository
{
    [TestFixture]
    public abstract class BaseRepositoryTest<T> : AbstractTransactionalSpringContextTests where T : BaseEntity, new()
    {
        public BaseRepositoryTest()
        {
            XmlConfigurator.Configure();
        } 

        public IBasicRepository<T> BasicRepository { get; set; }

        public ISessionFactory SessionFactory { get; set; }

        public ISchemaGeneration SchemaGeneration { get; set; }

        private DateTime TimeOnTestSetup { get; set; }


        protected ISession Session
        {
            get { return SessionFactory.GetCurrentSession(); }
        }

        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                           {
                               "assembly://Alstom.Repository/Alstom.Repository/Repository.xml",
                               "config://spring/objects"
                           };
            }
        }

        [SetUp]
        public void Init()
        {
            SchemaGeneration.GenerateDatabase();
            
            TimeOnTestSetup = DateTime.UtcNow;
            ThreadIdentity.SetCurrentThreadIdentity("Test Identity");
            SessionFactory.GetCurrentSession().BeginTransaction();
        }

        [TearDown]
        public void Cleanup()
        {
            SessionFactory.GetCurrentSession().Transaction.Commit();
            SessionFactory.GetCurrentSession().Transaction.Dispose();
        }

        public virtual void VerifyAuditInfoAfterInsert(T entity)
        {
            Assert.IsNotNull(entity);
            Assert.AreEqual("Test Identity", entity.AuditInfo.CreationUser);
            Assert.AreEqual("Test Identity", entity.AuditInfo.ModificationUser);
            Assert.GreaterOrEqual(entity.AuditInfo.CreationDate, TimeOnTestSetup);
            Assert.LessOrEqual(entity.AuditInfo.CreationDate, DateTime.UtcNow);
            Assert.GreaterOrEqual(entity.AuditInfo.ModificationDate, TimeOnTestSetup);
            Assert.LessOrEqual(entity.AuditInfo.ModificationDate, DateTime.UtcNow);
        }

        public virtual void VerifyAuditInfoAfterUpdate(T entity,DateTime updateTime, string modificationUser)
        {
            Assert.IsNotNull(entity);
            Assert.AreEqual(modificationUser, entity.AuditInfo.ModificationUser);
            Assert.LessOrEqual(entity.AuditInfo.ModificationDate, DateTime.UtcNow);
            Assert.GreaterOrEqual(entity.AuditInfo.ModificationDate, updateTime);
            
        }

        public virtual void VerifyAuditInfoAfterDelete(T entity, DateTime updateTime, string modificationUser)
        {
            Assert.IsNotNull(entity);
            Assert.IsTrue(entity.AuditInfo.IsDeleted);
            Assert.AreEqual(modificationUser, entity.AuditInfo.ModificationUser);
            Assert.LessOrEqual(entity.AuditInfo.ModificationDate, DateTime.UtcNow);
            Assert.GreaterOrEqual(entity.AuditInfo.ModificationDate, updateTime);

        }

        protected void ResetTransaction(bool rollbackCurrent)
        {
            if (rollbackCurrent)
            {
                Session.Transaction.Rollback();
            }
            else
            {
                Session.Flush();
                Session.Transaction.Commit();
            }

            var transaction = Session.BeginTransaction();
        }

        protected T RefreshEntityFromDatabase(int id)
        {
            RefreshTransaction();
            return BasicRepository.Load(id);
        }

        protected void RefreshTransaction()
        {
            SessionFactory.GetCurrentSession().Transaction.Commit();
            SessionFactory.GetCurrentSession().BeginTransaction();
        }

      
    }
}
