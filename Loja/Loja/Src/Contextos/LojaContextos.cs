using Microsoft.EntityFrameworkCore;
using Loja.Src.Modelos;

namespace Loja.Src.Contextos
{
    public class LojaContextos : DbContext
    {
        #region Atributos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        #endregion
        #region Construtores
        public LojaContextos(DbContextOptions<LojaContextos> opt) :
        base(opt)
        {
        }
        #endregion
    }
}

