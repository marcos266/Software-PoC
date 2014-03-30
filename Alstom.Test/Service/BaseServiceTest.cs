using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Configuration.Identity;
using Alstom.Configuration.SchemaGeneration;
using Alstom.Model;
using Alstom.Service;
using NUnit.Framework;
using Spring.Testing.NUnit;
using log4net.Config;

namespace Alstom.Test.Service
{
    [TestFixture]
    public abstract class BaseServiceTest<T> : AbstractDependencyInjectionSpringContextTests where T : BaseEntity
    {
        public IBasicService<T> BasicService { get; set; }
        
        public ISchemaGeneration SchemaGeneration { get; set; }

        public BaseServiceTest()
        {
            XmlConfigurator.Configure();
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            SchemaGeneration.GenerateDatabase();
            ThreadIdentity.SetCurrentThreadIdentity("Test Identity");
        }

        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                           {
                               "assembly://Alstom.Service/Alstom.Service/Service.xml",
                               "config://spring/objects"
                           };
            }
        }
    }
}
