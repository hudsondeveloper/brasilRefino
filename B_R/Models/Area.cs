using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class Area
    {
        public virtual int Id { get; set; }
        public virtual string codigo { get; set; }
        public virtual string nome { get; set; }
        private readonly IList<Equipamento> _equipamento;
        public virtual IEnumerable<Equipamento> Equipamento {get { return _equipamento; } }

        private readonly IList<LogArea> _logArea;
        public virtual IEnumerable<LogArea> LogArea { get { return _logArea; } }

        public Area()
        {
            _equipamento = new List<Equipamento>();
            _logArea = new List<LogArea>();
        }
    }
}