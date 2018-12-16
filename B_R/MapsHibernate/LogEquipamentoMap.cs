using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
 
        public class LogEquipamentoMap : ClassMap<LogEquipamento>
        {
            public LogEquipamentoMap()
            {
                Id(x => x.Id).GeneratedBy.Increment();
                Map(x => x.acao);
                References(x => x.equipamento).Not.LazyLoad();
                References(x => x.user).Not.LazyLoad();
                Map(x => x.alteracao);
                Map(x => x.anterior);
                Map(x => x.horario);
                Table("LogsEquipamento");
            }
       }
    
}