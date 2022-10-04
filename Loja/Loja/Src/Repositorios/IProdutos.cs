using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Src.Modelos;

namespace Loja.Src.Repositorios
{
    public interface IProdutos
    {
        Task<List<Produtos>> PegarTodosProdutosAsync();
        Task<Produtos> PegarProdutosPeloIdAsync(int id);
        Task NovoProdutosAsync(Produtos produtos);
        Task AtualizarProdutosAsync(Produtos produtos);
        Task DeletarProdutosAsync(int id);
    }
}