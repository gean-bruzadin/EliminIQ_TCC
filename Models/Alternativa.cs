using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliminIQ_TCC.Models
{
    public class Alternativa
    {
        [Key] public int Id_Alternativa { get; set; }
        public string Descricao_Alternativa { get; set; }
        public bool Status { get; set; }

        [ForeignKey("Pergunta")] public int Fk_Pergunta { get; set; }
        public Pergunta Pergunta { get; set; }
    }
}
