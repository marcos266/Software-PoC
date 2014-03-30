using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using Spring.Data.NHibernate;

namespace Alstom.Configuration.SchemaGeneration
{
    public interface ISchemaGeneration
    {
        LocalSessionFactoryObject LocalSessionFactoryObject { get; set; }
        void GenerateDatabase();
    }

    public class SchemaGeneration : ISchemaGeneration
    {
        public LocalSessionFactoryObject LocalSessionFactoryObject { get; set; }

        public void GenerateDatabase()
        {
            SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(LocalSessionFactoryObject.Configuration);

            schemaExport.Create(true, true);

        }

    }
}
