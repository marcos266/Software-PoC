using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace Alstom.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();

            
            IDataBaseSchemaCreation dataBaseSchemaCreation = ctx.GetObject("DataBaseSchemaCreation") as IDataBaseSchemaCreation;

            dataBaseSchemaCreation.RunDatabaseSchemaGeneration();

        }
    }
}
