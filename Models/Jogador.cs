using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Jogador
    {
        [Key]
        public int Id_Jogador { get; set; }
        public string Nome_Jogador { get; set; }
        public string Email_Jogador { get; set; }
        public string Senha_Jogador { get; set; }
    }
}
