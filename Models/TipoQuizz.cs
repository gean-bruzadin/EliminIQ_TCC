using System.ComponentModel.DataAnnotations;

namespace EliminIQ_TCC.Models
{
    public class TipoQuizz
    {
        [Key] public int Id_Tipo_Quiz { get; set; }
        public string Tema_Quiz { get; set; }

    }
}
