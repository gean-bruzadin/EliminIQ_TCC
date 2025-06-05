using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliminIQ_TCC.Models
{
    public class Pergunta
    {
        [Key] public int Id_Pergunta { get; set; }
        public string Descricao_Pergunta { get; set; }

        [ForeignKey("Quizz")] public int Fk_Quiz { get; set; }
        public Quizz Quizz { get; set; }

        public ICollection<Alternativa> Alternativa { get; set; }
    }
}
