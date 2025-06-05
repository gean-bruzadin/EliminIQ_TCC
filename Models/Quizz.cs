using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Quizz
    {
        [Key]
        public int Id_Quizz { get; set; }
        public string Nome_Quizz { get; set; }
        public int Qtd_perguntas { get; set; }
    }
}
