using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Dificuldade
    {
        [Key] public int Id_Dificuldade { get; set; }
        public string Nome_Dificuldade { get; set; }
    }
}
