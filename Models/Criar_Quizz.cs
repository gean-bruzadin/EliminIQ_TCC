using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Criar_Quizz
    {
        [Key] public int Fk_Usuario { get; set; }
        [Key] public int Fk_Quizz { get; set; }
    }
}
