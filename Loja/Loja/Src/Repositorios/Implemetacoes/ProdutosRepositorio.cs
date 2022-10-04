using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.Src.Contextos;
using Loja.Src.Modelos;
using Microsoft.EntityFrameworkCore;
namespace Loja.Src.Repositorios.Implementacoes
{

    public class ProdutosRepositorio : IProdutos
    {
        #region Atributos
        private readonly LojaContextos _contexto;
        #endregion Atributos
        #region Construtores
        public ProdutosRepositorio(LojaContextos contexto)
        {
            _contexto = contexto;
        }
        #endregion Construtores
        #region Métodos
        public async Task<List<Produtos>> PegarTodosProdutosAsync()
        {
            return await _contexto.Produtos.ToListAsync();
        }
        public async Task<Produtos> PegarProdutosPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Produto não encontrado");
            return await _contexto.Produtos.FirstOrDefaultAsync(t => t.Id_Produto == id);
            // funções auxiliares
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Produtos.FirstOrDefault(u => u.Id_Produto == id);
                return auxiliar != null;
            }
        }
        public async Task NovoProdutosAsync(Produtos produtos)
        {
            await _contexto.Produtos.AddAsync(
            new Produtos
            {
                Produto = produtos.Produto,
                Descricao = produtos.Descricao,
                Categoria = produtos.Categoria,
                Valor = produtos.Valor,
                Quantidade = produtos.Quantidade,
                Status = produtos.Status,
                Url_Imagem = produtos.Url_Imagem
            }) ;
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarProdutosAsync(Produtos produtos)
        {
           
            var produtosExistente = await PegarProdutosPeloIdAsync(produtos.Id_Produto);
            produtosExistente.Descricao = produtos.Descricao;
            _contexto.Produtos.Update(produtosExistente);
            await _contexto.SaveChangesAsync();
           
        }
        public async Task DeletarProdutosAsync(int id)
        {
            _contexto.Produtos.Remove(await PegarProdutosPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }
        #endregion Métodos
    }
}