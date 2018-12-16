using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
    class LogAreaMap : ClassMap<LogArea>
    {
        public LogAreaMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.acao);
            References(x => x.area).Not.LazyLoad(); ;
            References(x => x.user).Not.LazyLoad();
            Map(x => x.alteracao);
            Map(x => x.anterior);
            Map(x => x.horario);
            Table("LogsArea");
        }
    }
}