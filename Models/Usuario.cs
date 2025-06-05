using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Usuario
    {
        [Key] 
        public int Id_Usuario { get; set; }
        public string Nome_Usuario { get; set; }
        public string Email_Usuario { get; set; }
        public string Senha_Usuario { get; set; }
        public ICollection<Usuario_Quizz> Usuario_Quizzes { get; set; }
    }
}
