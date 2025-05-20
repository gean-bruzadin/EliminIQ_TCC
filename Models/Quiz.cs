using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Quiz
    {
        [Key]
        public int Id_Quiz { get; set; }
        public string Nome_Quiz { get; set; }
        public int Qtd_perguntas { get; set; }
    }
}
