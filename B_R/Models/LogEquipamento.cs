using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class LogEquipamento
    {

        public virtual int Id { get; set; }
        public virtual User user { get; set; }
        public virtual string acao { get; set; }
        public virtual string anterior { get; set; }
        public virtual string alteracao { get; set; }
        public virtual DateTime horario { get; set; }
        public virtual Equipamento equipamento { get; set; }
    }
}