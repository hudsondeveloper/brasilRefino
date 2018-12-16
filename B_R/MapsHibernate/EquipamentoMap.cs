using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
    class EquipamentoMap : ClassMap<Equipamento>
    {
        public EquipamentoMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.descricao);
            Map(x => x.descricao_equipamento);
            Map(x => x.reg);
            Map(x => x.situacao).CustomType<situacao>();
            Map(x => x.status_Doc).CustomType<status_Doc>();
            Map(x => x.tag);
            References(x => x.area).Not.LazyLoad(); ;
            Table("Equipamentos");
        }
    }
}