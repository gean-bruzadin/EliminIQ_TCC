using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliminIQ_TCC.Models
{
    public class Quizz
    {
        [Key] 
        public int Id_Quiz { get; set; }
        public string Nome_Quiz { get; set; }
        public string Privacidade_Quiz { get; set; }
        public string Dificuldade_Quiz { get; set; }

        [ForeignKey("TipoQuizz")] public int Fk_Tipo_Quiz { get; set; }
        public TipoQuizz TipoQuizz { get; set; }

        [ForeignKey("Dificuldade")] public int Fk_Dificuldade { get; set; }
        public Dificuldade Dificuldade { get; set; }

        [ForeignKey("Privacidade")] public int Fk_Privacidade { get; set; }
        public Privacidade Privacidade { get; set; }

        public ICollection<Pergunta> Perguntas { get; set; }
        public ICollection<Usuario_Quizz> Usuario_Quizzes { get; set; }
    }
}
