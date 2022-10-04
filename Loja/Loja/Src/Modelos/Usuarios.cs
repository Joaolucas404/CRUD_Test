using Loja.Src.Utilidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loja.Src.Modelos
{
    [Table("tb_Usuarios")]
    public class Usuario
    {
        #region Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Usuario { get; set; }
        public string Nome { get; set; }
        public string Log_in { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Documento { get; set; }
        public Portadores Tipo { get; set; }


        [JsonIgnore, InverseProperty("Criador")]
        public List<Compras> MinhasCompras { get; set; }

        #endregion
    }

}
