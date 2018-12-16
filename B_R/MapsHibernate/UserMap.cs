using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
     class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity(); 
            Map(x => x.Nome);
            Map(x => x.Email);
            Map(x => x.Cargo);
            Map(x => x.CodCracha);
            Map(x => x.Sexo);
            Map(x => x.cpf);
            Map(x => x.Role);
            Map(x => x.senha);
            HasMany(x => x.LogArea).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete(); ;
            HasMany(x => x.LogEquipamento).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete(); ;
            Table("Users");
        }
    }
}