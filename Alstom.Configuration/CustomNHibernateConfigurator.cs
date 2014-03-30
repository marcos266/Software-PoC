using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Alstom.Configuration.EventListener;
using NHibernate.Cfg;

using NHibernate.Event;

namespace Alstom.Configuration
{
    public class CustomNHibernateConfigurator
    {
        public static void ConfigureMappingsAndEventListeners(NHibernate.Cfg.Configuration config,
                                                              string[] annotatedMappingAssemblies)
        {

            

            foreach (String assembly in annotatedMappingAssemblies)
            {
                MemoryStream stream = new MemoryStream();

                NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
                NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(stream, Assembly.Load(assembly));

                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                String st = reader.ReadToEnd();
                Console.Write(st);
                stream.Position = 0;
                config.AddInputStream(stream);
            }

            config.Properties["show_sql"] = "True";
            config.Properties["hbm2ddl.keywords"] = "none";

            config.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new UpdateEventListener() };
            config.EventListeners.DeleteEventListeners = new DeleteEventListener[] { new DeleteEventListener() };
            config.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new InsertEventListener() };
        } 
    }
}
