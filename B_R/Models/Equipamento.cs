using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class Equipamento
    {
        public virtual int Id { get; set; }
        public virtual int reg { get; set; }
        public virtual string tag { get; set; }
        public virtual string descricao_equipamento { get; set; }
        public virtual situacao situacao { get; set; }
        public virtual status_Doc status_Doc { get; set; }
        public virtual string descricao { get; set; }
        public virtual Area area { get; set; }

        private readonly IList<LogEquipamento> _logEquipamento;
        public virtual IEnumerable<LogEquipamento> logEquipamento { get { return _logEquipamento; } }

        public Equipamento()
        {
            area = new Area();
           _logEquipamento = new List<LogEquipamento>();
        }
    }

    public enum status_Doc
    {
        ATUALIZADO,
        PENDENTE
    }

    public enum situacao
    {
        ATIVO,
        FORA_DE_OPERACAO
    }
}