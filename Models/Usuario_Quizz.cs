using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Usuario_Quizz
    {
        [Key] public int Fk_Usuario { get; set; }
        [Key] public int Fk_Quizz { get; set; }
        public int Vida { get; set; }
        public int StatusVida { get; set; }
        public int Respaw { get; set; }
    }
}