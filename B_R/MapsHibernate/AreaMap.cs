using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
    class AreaMap : ClassMap<Area>
    {
        public AreaMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.nome);
            Map(x => x.codigo);
            HasMany(x => x.Equipamento).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete(); ;
            HasMany(x => x.LogArea).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete();

            Table("Areas");
        }
    }
}