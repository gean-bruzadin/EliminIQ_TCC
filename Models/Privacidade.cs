using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class Privacidade
    {
        [Key] public int Id_Privacidade { get; set; }
        public string Nome_Privacidade { get; set; }
    }
}
