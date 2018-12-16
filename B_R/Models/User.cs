using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Role { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Cargo { get; set; }
        public virtual string Sexo { get; set; }
        public virtual string cpf { get; set; }
        public virtual string CodCracha { get; set; }
        public virtual string senha { get; set; }


        private readonly IList<LogArea> _logArea;
        public virtual IEnumerable<LogArea> LogArea { get { return _logArea; } }


        private readonly IList<LogEquipamento> _logEquipamento;
        public virtual IEnumerable<LogEquipamento> LogEquipamento { get { return _logEquipamento; } }


        public User()
        {
            _logEquipamento = new List<LogEquipamento>();
            _logArea = new List<LogArea>();
        }
    }
}