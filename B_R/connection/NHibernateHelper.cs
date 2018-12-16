using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class NHibernateHelper
    {
        // @"Server=postgres://znitpwbb:nDmzcRlUqQ6yWQv6OLKNg-IBgIn1esDo@pellefant.db.elephantsql.com:5432/znitpwbb;  Port=5432;User Id = znitpwbb;Password=nDmzcRlUqQ6yWQv6OLKNg-IBgIn1esDo;    Database=BR"
        // private static string ConnectionString = "Server=localhost; Port=5432; User Id=postgres; Password=root; Database=BR";

        private static ISessionFactory session;


        public static ISession OpenSession()
        {
            if (session != null)
                return session.OpenSession();

            session = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                  .ConnectionString(@"Server=pellefant.db.elephantsql.com; 
                                    Port=5432;
                                    User Id=znitpwbb; 
                                    Password=nDmzcRlUqQ6yWQv6OLKNg-IBgIn1esDo; 
                                    Database=znitpwbb")
                              .ShowSql()
                )
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Area>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Equipamento>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogArea>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogEquipamento>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)
                                               /* .Create(false,true)*/)
                .BuildSessionFactory();
            return session.OpenSession();
        }


    }
}