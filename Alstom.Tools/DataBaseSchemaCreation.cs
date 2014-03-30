using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Spring.Data.NHibernate;

namespace Alstom.Tools
{
    public interface IDataBaseSchemaCreation
    {
        LocalSessionFactoryObject SessionFactory { get; set; }
        void RunDatabaseSchemaGeneration();
    }

    public class DataBaseSchemaCreation : IDataBaseSchemaCreation
    {
        public LocalSessionFactoryObject SessionFactory { get; set; }


        public void RunDatabaseSchemaGeneration()
        {
            SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(SessionFactory.Configuration);
                
            schemaExport.Create(true,true);

        }
    }
}
