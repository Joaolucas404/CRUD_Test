using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loja.Src.Modelos
{

    [Table("tb_Produtos")]
    public class Produtos
    {
        #region Atributos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Produto { get; set; }
        public string Produto { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Valor { get; set; }
        public string Quantidade { get; set; }
        public string Status { get; set; }
        public string Url_Imagem { get; set; }

        #endregion
    }

}
