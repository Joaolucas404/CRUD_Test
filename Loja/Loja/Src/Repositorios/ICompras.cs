using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Src.Modelos;

namespace Loja.Src.Repositorios
{
    public interface ICompras
    {
        Task<List<Compras>> PegarTodasComprasAsync();
        Task<Compras> PegarComprasPeloIdAsync(int id);
        Task NovaComprasAsync(Compras compras);
        Task AtualizarComprasAsync(Compras compras);
        Task DeletarComprasAsync(int id);
    }
}
