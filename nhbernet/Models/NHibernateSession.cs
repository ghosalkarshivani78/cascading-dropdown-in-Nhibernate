using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NHibernate.Cfg;
using NHibernate;

namespace nhbernet.Models
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\nhybermet.config");
            configuration.Configure(configurationPath);
           var bookConfigurationFile = HttpContext.Current.Server.MapPath(@"~\user.hbm.xml");
           var cityConfigurationFile = HttpContext.Current.Server.MapPath(@"~\city.hbm.xml");
           var stateConfigurationFile = HttpContext.Current.Server.MapPath(@"~\state.hbm.xml");
            configuration.AddFile(bookConfigurationFile);
            configuration.AddFile(cityConfigurationFile);
            configuration.AddFile(stateConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();

        }
    }
}